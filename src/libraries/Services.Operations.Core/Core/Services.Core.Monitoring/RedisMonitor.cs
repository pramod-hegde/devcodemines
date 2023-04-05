using Services.Cache.Contracts;
using Services.Core.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Warden;
using Warden.Watchers;
using Warden.Watchers.Redis;

namespace Services.Core.Monitoring
{
    [Export(typeof(ICompositionPart))]
    [ComponentType(DependentComponentTypes.RedisCache)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class RedisMonitor : AbstractMonitorBase<IRedisCacheProviderConfiguration, WardenWatchDog>
    {
        public override string Id => "RedisCacheMonitor";

        ConfigurationOptions RedisProperties => ConfigurationOptions.Parse(_setting?.ConnectionString);

        public override async Task<IEnumerable<DependentComponentHealth>> CheckHealth ()
        {
            await _watchDog.ExecuteAsync();
            return _health;
        }

        public override IDependentComponent CreatePart ()
        {
            return new RedisMonitor();
        }

        IWatcher Watcher
        {
            get
            {
                IWatcher w = null;                

                if (_needsCaching && _cacheManager!=null)
                {
                    var k = RedisProperties.ToString(false);
                    if (_cacheManager.Contains(k))
                    {
                        w = (IWatcher)_cacheManager.Get(k);
                    }
                    else
                    {
                        w = RedisWatcher.Create(connectionString: _setting.ConnectionString, database: (int)_setting.DatabaseId);
                        _cacheManager.Insert(k, w, new DefaultCacheItemPolicy());
                    }
                }
                else
                {
                    w = RedisWatcher.Create(connectionString: _setting.ConnectionString, database: (int)_setting.DatabaseId);
                }

                return w;
            }
        }

        public override Task Initialize ()
        {
            if (_watchDog == null)
            {
                _watchDog = new WardenWatchDog();
            }

            _watchDog.AddWatcher(Watcher);
            _watchDog.OnSuccess(c => OnCompletion(c));
            _watchDog.OnFailure(c => OnCompletion(c));
            _watchDog.OnError(e => HandleError(e));

            return Task.CompletedTask;
        }

        private void HandleError (Exception e)
        {
            _health.Add(new DependentComponentHealth
            {
                ComponentType = DependentComponentTypes.RedisCache,
                IsValid = false,
                Connection = RedisProperties.ToString(false),
                Identifier = RedisProperties.ClientName,
                ErrorDetails = e.ToString()
            });
        }

        private void OnCompletion (IWardenCheckResult c)
        {
            _health.Add(new DependentComponentHealth
            {
                ComponentType = DependentComponentTypes.RedisCache,
                IsValid = c.IsValid,
                Connection = RedisProperties.ToString(false),
                Identifier = RedisProperties.ClientName,
                ErrorDetails = c.Exception?.ToString(),
                Description = c.WatcherCheckResult?.Description,
                ExecutionTime = c.ExecutionTime,
                StartedAt = c.StartedAt,
                CompletedAt = c.CompletedAt
            });
        }
    }
}
