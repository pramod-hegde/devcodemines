using System;

namespace Services.Core.Contracts
{
    public interface ICacheItemPolicy
    {
        bool IsAbsoluteExpiration { get; }
        DateTimeOffset AbsoluteExpiration { get; }
        CacheItemPriority Priority { get; }
        CacheEntryRemovedCallback RemovedCallback { get; }
        TimeSpan SlidingExpiration { get; }
        CacheEntryUpdateCallback UpdateCallback { get; }
    }

    public delegate void CacheEntryRemovedCallback (CacheEntryRemovedArguments arguments);

    public delegate void CacheEntryUpdateCallback (CacheEntryUpdateArguments arguments);
}
