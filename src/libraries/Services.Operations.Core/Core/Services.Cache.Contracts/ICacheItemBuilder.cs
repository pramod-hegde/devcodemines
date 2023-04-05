namespace Services.Cache.Contracts
{
    public interface ICacheItemBuilder
    {
        dynamic BuildCacheItem (object value);
    }
}
