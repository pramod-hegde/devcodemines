namespace Services.Core.Contracts
{
    public class CacheEntryRemovedArguments
    {
        public CacheEntryRemovedArguments (CacheStore store, CacheEntryRemovedReason reason, object cacheItem)
        {
            Store = store;
            RemovedReason = reason;
            CacheItem = cacheItem;
        }

        public object CacheItem { get; }
        public CacheEntryRemovedReason RemovedReason { get; }
        public CacheStore Store { get; }
    }
}
