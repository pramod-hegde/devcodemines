using Services.Core.Contracts;
using System;

namespace Services.Cache.Memory
{
    class DefaultCacheItemPolicy : ICacheItemPolicy
    {
        DateTimeOffset ICacheItemPolicy.AbsoluteExpiration => DateTime.UtcNow.Add(TimeSpan.FromHours(12));

        CacheItemPriority ICacheItemPolicy.Priority => CacheItemPriority.Default;

        CacheEntryRemovedCallback ICacheItemPolicy.RemovedCallback => null;

        TimeSpan ICacheItemPolicy.SlidingExpiration => TimeSpan.FromHours(12);

        CacheEntryUpdateCallback ICacheItemPolicy.UpdateCallback => null;

        bool ICacheItemPolicy.IsAbsoluteExpiration => true;
    }
}
