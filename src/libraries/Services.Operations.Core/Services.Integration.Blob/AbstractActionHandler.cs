﻿using Services.Core.Logging.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Diagnostics;
using Services.Integration.Core;
using Services.Data.Blob;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Services.Integration.Blob
{
    public abstract class AbstractActionHandler<TSvcRequest, TSvcResponse> : IExternalIntegrationAction
    {
        protected IBlobActionConfig _config;

        List<Type> _registeredResponseBuilders = null;

        public AbstractActionHandler()
        {
            _registeredResponseBuilders = new List<Type>();
        }

        async Task<object> IExternalIntegrationAction.ExecuteAsync<TIn, TConfig>(TIn input, TConfig config)
        {
            _config = (IBlobActionConfig)config;

            if (_config == default)
            {
                throw new ExternalIntegrationException("BlobActionConfig is null");
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

                return responseBuilder.CreateResponse(input, externalServiceResponse);
            }
            catch
            (Exception ex)
            {
                watch.Stop();
                LogDependency(ServiceAction, "AzureBlob", ActionName, watch.Elapsed, false);
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
            if (BlobSettings.EnableRequestResponseLogging)
            {
                LogEvent($"{ActionName} request", new KeyValuePair<string, string>("Request", Serialize(externalServiceRequest)));
            }

            LogTrace($"Invoking {ActionName}");

            watch.Start();
            var externalServiceResponse = await Invoke(externalServiceRequest);
            watch.Stop();

            LogDependency(ServiceAction, "AzureBlob", ActionName, watch.Elapsed, true);

            if (BlobSettings.EnableRequestResponseLogging)
            {
                LogEvent($"{ActionName} response", new KeyValuePair<string, string>("Response", Serialize(externalServiceResponse)));
            }

            return externalServiceResponse;
        }

        protected async Task<List<T>> ReadDataAsync<T>(BlobQueryConfiguration blobConfig)
        {
            try
            {
                var adapterConfig = BlobSettings.AuthenticationConfig.AuthenticationCallback();
                adapterConfig.CacheManager = _config.Cache;
                adapterConfig.BlobHandlers = new[] { _config.BlobHandler };

                using var adapter = await new BlobSourceAdapterFactory<IMemoryCache>().CreateAsync(adapterConfig, null, CancellationToken.None);

                var result = await adapter.ReadAsync<T>(blobConfig, CancellationToken.None);

                return result?.ToList();
            }
            catch (Exception ex)
            {
                _config?.Logger?.LogException(ex);
                throw;
            }
        }

        protected async Task WriteAsync<T>(T data, BlobWriteSettings writeSettings)
        {
            var adapterConfig = BlobSettings.AuthenticationConfig.AuthenticationCallback();
            adapterConfig.CacheManager = _config.Cache;
            adapterConfig.BlobHandlers = new[] { _config.BlobHandler };

            using var adapter = await new BlobSourceAdapterFactory<IMemoryCache>().CreateAsync(adapterConfig, null, CancellationToken.None);
            await adapter.WriteAsync(data, writeSettings, CancellationToken.None);
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
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            return JsonConvert.SerializeObject(obj, settings);
        }

        protected abstract TSvcRequest GetRequest<TIn>(TIn input);
        protected abstract Task<TSvcResponse> Invoke(TSvcRequest request);
        protected abstract string ActionName { get; }
        protected abstract string ServiceAction { get; }

        IExternalIntegrationMetadata _blobActionMetadata;
        protected IExternalIntegrationMetadata BlobActionMetadata
        {
            get
            {
                if (_blobActionMetadata == null)
                {
                    _blobActionMetadata = _config.OnActionInvoke(ServiceAction);
                }
                return _blobActionMetadata;
            }
        }

        protected IBlobConfiguration BlobSettings => BlobActionMetadata.Settings as IBlobConfiguration;

        IResponseBuilder ResponseBuilder
        {
            get
            {
                if (!_registeredResponseBuilders.Any())
                {
                    var type = typeof(IResponseBuilder);
                    //Added workaround to skip Microsoft.Azure assembly
                    var types = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.FullName.ToLower().Contains("microsoft.azure"))
                        .SelectMany(x => x.GetTypes()).Where(p => type.IsAssignableFrom(p));

                    if (types.Any())
                    {
                        _registeredResponseBuilders.AddRange(types.ToList());
                    }
                }

                foreach (var m in _registeredResponseBuilders)
                {
                    var a = m.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(ExternalIntegrationActionAttribute));

                    if (a == null)
                    {
                        continue;
                    }

                    if (a.ConstructorArguments[0].Value.ToString() == ServiceAction)
                    {
                        return (IResponseBuilder)Activator.CreateInstance(m);
                    }
                }

                return null;
            }
        }
    }
}
