namespace Services.Core.Common
{
    public abstract class ClientCacheHandler<TCache> : ICacheHandler<TCache>
    {
        protected TCache _cache;

        public ClientCacheHandler(TCache cache)
        {
            _cache = cache;
        }

        public abstract bool Contains(string key);
        public abstract object Get(string key);
        public abstract void Insert(string key, object value);
    }
}