namespace Services.Core.Contracts
{
    public class CacheEntryUpdateArguments
    {
        public CacheEntryUpdateArguments (CacheStore store, CacheEntryRemovedReason reason, string key)
        {
            Store = store;
            RemovedReason = reason;
            Key = key;
        }

        public string Key { get; }
        public CacheEntryRemovedReason RemovedReason { get; }
        public CacheStore Store { get; }
        public object UpdatedCacheItem { get; set; }
        public object UpdatedCacheItemPolicy { get; set; }
    }
}
