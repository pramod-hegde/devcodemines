namespace Services.Core.Contracts
{
    public enum CacheEntryRemovedReason
    {
        Removed = 0,
        Expired = 1,
        Evicted = 2,
        ChangeMonitorChanged = 3,
        CacheSpecificEviction = 4
    }
}
