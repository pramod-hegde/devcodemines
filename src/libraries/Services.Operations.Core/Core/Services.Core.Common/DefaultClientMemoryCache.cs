using Microsoft.Extensions.Caching.Memory;
using System;

namespace Services.Core.Common
{
    public sealed class DefaultClientMemoryCache : ClientCacheHandler<IMemoryCache>
    {
        public DefaultClientMemoryCache(IMemoryCache cache) : base(cache)
        {
        }

        public override bool Contains(string key)
        {
            return _cache.TryGetValue(key, out object temp);
        }

        public override object Get(string key)
        {
            return _cache?.Get(key);
        }

        public override void Insert(string key, object value)
        {
            _cache?.Set(key, value, DateTimeOffset.UtcNow.AddHours(12));
        }
    }
}