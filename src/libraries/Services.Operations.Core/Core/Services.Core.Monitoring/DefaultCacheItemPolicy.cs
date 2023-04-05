using Services.Core.Contracts;
using System;

namespace Services.Core.Monitoring
{
    sealed class DefaultCacheItemPolicy : ICacheItemPolicy
    {
        public bool IsAbsoluteExpiration => true;

        public DateTimeOffset AbsoluteExpiration => new DateTimeOffset(DateTime.UtcNow.AddHours(8));

        public CacheItemPriority Priority => CacheItemPriority.Default;

        public CacheEntryRemovedCallback RemovedCallback => null;

        public TimeSpan SlidingExpiration => TimeSpan.Zero;

        public CacheEntryUpdateCallback UpdateCallback => null;
    }
}
