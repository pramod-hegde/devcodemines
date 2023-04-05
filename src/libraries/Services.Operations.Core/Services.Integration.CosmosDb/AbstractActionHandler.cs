using Microsoft.Azure.Documents;
using Services.Core.Logging.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Services.Integration.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Services.Data.CosmosDb;

namespace Services.Integration.CosmosDb
{
    public abstract class AbstractActionHandler<TSvcRequest, TSvcResponse> : IExternalIntegrationAction
    {
        protected ICosmosDbActionConfig _config;

        List<Type> _registeredResponseBuilders = null;

        public AbstractActionHandler()
        {
            _registeredResponseBuilders = new List<Type>();
        }

        async Task<object> IExternalIntegrationAction.ExecuteAsync<TIn, TConfig>(TIn input, TConfig config)
        {
            _config = (ICosmosDbActionConfig)config;

            if (_config == default)
            {
                throw new ExternalIntegrationException("CosmosDbActionConfig is null");
            }

            Stopwatch watch = new Stopwatch();

            try
            {
                var externalServiceRequest = GetRequest(input);
                if (externalServiceRequest == null)
                {
                    throw new ExternalIntegrationException($"{ServiceAction} request is null");
                }

                TSvcResponse externalServiceResponse = await Execute(watch, externalServiceRequest);

                var responseBuilder = ResponseBuilder;

                if (responseBuilder == null)
                {
                    return externalServiceResponse;
                }

                responseBuilder?.SetConfig(_config);
                return responseBuilder.CreateResponse(input, externalServiceResponse);
            }
            catch (Exception ex)
            {
                watch.Stop();
                LogDependency(ServiceAction, "Azure CosmosDB", ActionName, watch.Elapsed, false);
                LogException(ex);
                throw;
            }
        }

        private void LogException(Exception ex)
        {
            _config?.Logger?.LogException(ex);
        }

        private async Task<TSvcResponse> Execute(Stopwatch watch, TSvcRequest externalServiceRequest)
        {
            if (CosmosDbSettings.EnableRequestResponseLogging)
            {
                LogEvent($"{ActionName} request", new KeyValuePair<string, string>("Request", Serialize(externalServiceRequest)));
            }

            LogTrace($"Invoking {ActionName}");

            watch.Start();
            var externalServiceResponse = await Invoke(externalServiceRequest);
            watch.Stop();

            LogDependency(ServiceAction, "Azure CosmosDB", ActionName, watch.Elapsed, true);

            if (CosmosDbSettings.EnableRequestResponseLogging)
            {
                LogEvent($"{ActionName} response", new KeyValuePair<string, string>("Response", Serialize(externalServiceResponse)));
            }

            return externalServiceResponse;
        }

        protected async Task<List<T>> ReadDataAsync<T>(CosmosDbQueryConfiguration queryConfiguration)
        {
            try
            {
                var adapterConfig = CosmosDbSettings.AuthenticationConfig.AuthenticationCallback();
                adapterConfig.CacheManager = _config.Cache;
                adapterConfig.Handlers = new[] { _config.StorageHandler };
                adapterConfig.UseReadOnlyConnection = CosmosDbSettings.EnableReadFromPreferredLocations;
                adapterConfig.PreferredReadLocations = CosmosDbSettings.PreferredLocations;

                using var adapter = await new CosmosDbSourceAdapterFactory<IMemoryCache>().CreateAsync(adapterConfig, null, CancellationToken.None);

                var result = await adapter.ReadAsync<T>(queryConfiguration, CancellationToken.None);

                return result?.ToList();
            }
            catch (Exception ex)
            {
                _config?.Logger?.LogException(ex);
                throw;
            }
        }

        protected async Task SaveDataAsync<T>(T data)
        {
            try
            {
                var adapterConfig = CosmosDbSettings.AuthenticationConfig.AuthenticationCallback();
                adapterConfig.CacheManager = _config.Cache;
                adapterConfig.Handlers = new[] { _config.StorageHandler };

                using var adapter = await new CosmosDbSourceAdapterFactory<IMemoryCache>().CreateAsync(adapterConfig, null, CancellationToken.None);

                CosmosDbWriteSettings settings = new()
                {
                    Action = CosmosDbAction.Upsert
                };

                await adapter.WriteAsync(data, settings, CancellationToken.None);
            }
            catch (DocumentClientException de)
            {
                if ((int)de.StatusCode != 429 || (int)de.StatusCode != 408)
                {
                    _config?.Logger?.LogEvent("Unhandled DocumentClientException", new Dictionary<string, object>
                    {
                        { "StatusCode", $"{(int)de.StatusCode}"},
                        { "DocumentToSave", JsonConvert.SerializeObject(data)}
                    });
                    throw;
                }

                await RetrySave(data, de);
            }
            catch (AggregateException ae)
            {
                if (!(ae.InnerException is DocumentClientException))
                {
                    _config?.Logger?.LogEvent("Unhandled exception while saving validated agreement payload", new Dictionary<string, object>
                    {
                        { "StackTrace", $"{ae.StackTrace}"},
                        { "DocumentToSave", JsonConvert.SerializeObject(data)}
                    });
                    throw;
                }

                DocumentClientException de = (DocumentClientException)ae.InnerException;
                if ((int)de.StatusCode != 429 || (int)de.StatusCode != 408)
                {
                    _config?.Logger?.LogEvent("Unhandled DocumentClientException", new Dictionary<string, object>
                    {
                        { "StatusCode", $"{(int)de.StatusCode}"},
                        { "DocumentToSave", JsonConvert.SerializeObject(data)}
                    });
                    throw;
                }

                await RetrySave(data, de);
            }
        }

        private async Task RetrySave<T>(T data, DocumentClientException de)
        {
            // retry after the specified time
            await Task.Delay(de.RetryAfter);
            await SaveDataAsync(data);
        }

        private async Task RetryDelete(string documentId, string partitionKeyValue, DocumentClientException de)
        {
            // retry after the specified time
            await Task.Delay(de.RetryAfter);
            await DeleteDataAsync(documentId, partitionKeyValue);
        }

        protected async Task DeleteDataAsync(string documentId, string partitionKeyValue)
        {
            try
            {
                var adapterConfig = CosmosDbSettings.AuthenticationConfig.AuthenticationCallback();
                adapterConfig.CacheManager = _config.Cache;
                adapterConfig.Handlers = new[] { _config.StorageHandler };

                using var adapter = await new CosmosDbSourceAdapterFactory<IMemoryCache>().CreateAsync(adapterConfig, null, CancellationToken.None);

                CosmosDbWriteSettings settings = new CosmosDbWriteSettings
                {
                    Action = CosmosDbAction.Delete,
                    PartitionKeyValue = partitionKeyValue
                };

                await adapter.WriteAsync(documentId, settings, CancellationToken.None);
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode != HttpStatusCode.TooManyRequests || de.StatusCode != HttpStatusCode.RequestTimeout)
                {
                    _config?.Logger?.LogEvent($"Unhandled {nameof(DocumentClientException)} while deleting document.", new Dictionary<string, object>
                    {
                        { "StatusCode", $"{(int)de.StatusCode}"},
                        { "DocumentIdToDelete", documentId}
                    });
                    throw;
                }

                await RetryDelete(documentId, partitionKeyValue, de);
            }
            catch (AggregateException ae)
            {
                if (!(ae.InnerException is DocumentClientException))
                {
                    _config?.Logger?.LogEvent($"Unhandled exception while deleting document.", new Dictionary<string, object>
                    {
                        { "StackTrace", $"{ae.StackTrace}"},
                        { "DocumentIdToDelete", documentId}
                    });
                    throw;
                }

                DocumentClientException de = (DocumentClientException)ae.InnerException;
                if (de.StatusCode != HttpStatusCode.TooManyRequests || de.StatusCode != HttpStatusCode.RequestTimeout)
                {
                    _config?.Logger?.LogEvent($"Unhandled {nameof(DocumentClientException)} while deleting document.", new Dictionary<string, object>
                    {
                        { "StatusCode", $"{(int)de.StatusCode}"},
                        { "DocumentIdToDelete", documentId}
                    });
                    throw;
                }

                await RetryDelete(documentId, partitionKeyValue, de);
            }
        }

        protected void LogTrace(string m)
        {
            _config.Logger?.LogTrace(m);
        }

        protected void LogEvent(string m, params KeyValuePair<string, string>[] properties)
        {
            var p = new Dictionary<string, object>();
            foreach (var x in properties)
            {
                p.Add(x.Key, x.Value);
            }
            _config.Logger?.LogEvent(m, additionalProperties: p);
        }

        protected void LogDependency(string dependency, string type, string command, TimeSpan timespent, bool success)
        {
            _config.Logger?.LogDependency(dependency, timespent, success, dependencyType: type, command: command);
        }

        string Serialize<T>(T obj)
        {
            if (obj == null)
                return null;

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                Formatting = Formatting.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            return JsonConvert.SerializeObject(obj, settings);
        }

        protected abstract TSvcRequest GetRequest<TIn>(TIn input);
        protected abstract Task<TSvcResponse> Invoke(TSvcRequest request);
        protected abstract string ActionName { get; }
        protected abstract string ServiceAction { get; }

        IExternalIntegrationMetadata _cosmosDbActionMetadata;
        protected IExternalIntegrationMetadata CosmosDbActionMetadata
        {
            get
            {
                if (_cosmosDbActionMetadata == null)
                {
                    _cosmosDbActionMetadata = _config.OnActionInvoke(ServiceAction);
                }
                return _cosmosDbActionMetadata;
            }
        }

        protected ICosmosDbConfiguration CosmosDbSettings => CosmosDbActionMetadata.Settings as ICosmosDbConfiguration;

        IResponseBuilder ResponseBuilder
        {
            get
            {
                if (!_registeredResponseBuilders.Any())
                {
                    var type = typeof(IResponseBuilder);
                    var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(p => type.IsAssignableFrom(p));

                    if (types.Any())
                    {
                        _registeredResponseBuilders.AddRange(types.ToList());
                    }
                }

                foreach (var m in _registeredResponseBuilders)
                {
                    var a = m.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(ExternalIntegrationActionAttribute) && a.ConstructorArguments[0].Value.ToString() == ServiceAction);

                    if (a == null)
                    {
                        continue;
                    }

                    return (IResponseBuilder)Activator.CreateInstance(m);
                }

                return null;
            }
        }
    }
}
