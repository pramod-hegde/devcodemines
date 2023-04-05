namespace Services.Core.Contracts
{
    public enum CacheStore
    {
        None = 0,
        MemoryCache = 1,
        Redis = 2,
        ///SqlServer = 4
        /// and any other store 
    }
}
