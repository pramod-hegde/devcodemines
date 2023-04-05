using Services.Core.Contracts;

namespace Services.Core.Common
{
    public sealed class CustomClientMemoryCache : ClientCacheHandler<ICacheManager>
    {
        public CustomClientMemoryCache(ICacheManager cache) : base(cache)
        {
        }

        public override bool Contains(string key)
        {
            return (bool)(_cache?.Contains(key));
        }

        public override object Get(string key)
        {
            return _cache?.Get(key);
        }

        public override void Insert(string key, object value)
        {
            _cache.Insert(key, value);
        }
    }
}