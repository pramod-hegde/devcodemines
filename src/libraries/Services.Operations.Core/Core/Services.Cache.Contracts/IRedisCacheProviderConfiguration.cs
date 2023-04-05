namespace Services.Cache.Contracts
{
    public interface IRedisCacheProviderConfiguration
    {
        string ConnectionString { get; }
        string Host { get; }
        int Port { get; }
        string Password { get; }
        long DatabaseId { get; }
        ICacheItemBuilder CacheBuilder { get; }
        bool EnableAutoDistribution { get; }
        int MaxDistributionIndex { get; }
    }
}
