using Services.Core.Contracts;
using System;
using System.ComponentModel.Composition;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace Services.Cache.Memory
{
    [Export(typeof(ICompositionPart))]
    [StoreType(CacheStore.MemoryCache)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class MemoryCacheManager : ICacheManager
    {
        MemoryCache _cache = MemoryCache.Default;

        object _mutex = new object();

        object ICacheManager.this[string key]
        {
            get
            {
                if (_cache == null || !_cache.Contains(key))
                {
                    return null;
                }
                return _cache.Get(key);
            }
        }

        int ICacheManager.Count
        {
            get
            {
                if (_cache == null)
                {
                    return -1;
                }

                return (int)_cache.GetCount();
            }
        }

        string ICompositionPart.Id => "MemCache";

        bool ICacheManager.Contains (string key)
        {
            return _cache.Contains(key);
        }

        Task<bool> ICacheManager.ContainsAsync (string key)
        {
            throw new NotSupportedException();
        }

        object ICacheManager.Get (string key)
        {
            if (_cache == null || !_cache.Contains(key))
            {
                return null;
            }
            return _cache.Get(key);
        }

        T ICacheManager.Get<T> (string key)
        {
            throw new NotSupportedException();
        }

        async Task<object> ICacheManager.GetAsync (string key)
        {
            if (_cache == null || !_cache.Contains(key))
            {
                return null;
            }

            return await Task.Run(() => _cache.Get(key));
        }

        Task<T> ICacheManager.GetAsync<T> (string key)
        {
            throw new NotSupportedException();
        }

        void ICacheManager.Initialize<T> (T configuration)
        {
            throw new NotSupportedException();
        }

        void ICacheManager.Insert (string key, object value)
        {
            if (_cache != null && !_cache.Contains(key))
            {
                lock (_mutex)
                {
                    _cache.Add(key, value, DateTime.UtcNow.Add(new TimeSpan(24, 0, 0)));
                };
            }
        }

        void ICacheManager.Insert (string key, object value, ICacheItemPolicy policy)
        {
            if (_cache != null && !_cache.Contains(key))
            {
                lock (_mutex)
                {
                    _cache.Add(key, value, new CacheItemPolicyBuilder(policy).Build());
                }
            }
        }

        async Task ICacheManager.InsertAsync (string key, object value)
        {
            if (_cache != null && !_cache.Contains(key))
            {
                await Task.Run(() => _cache.Add(key, value, DateTime.UtcNow.Add(new TimeSpan(24, 0, 0))));
            }
        }

        async Task ICacheManager.InsertAsync (string key, object value, ICacheItemPolicy policy)
        {
            if (_cache != null && !_cache.Contains(key))
            {
                await Task.Run(() => _cache.Add(key, value, new CacheItemPolicyBuilder(policy).Build()));
            }
        }

        void ICacheManager.Remove (string key)
        {
            if (_cache != null && _cache.Contains(key))
            {
                lock (_mutex)
                {
                    _cache.Remove(key);
                }
            }
        }
    }
}
