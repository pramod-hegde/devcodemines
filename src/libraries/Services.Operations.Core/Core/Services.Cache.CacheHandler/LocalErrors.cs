using Services.Core.Common;
using System;

namespace Services.Cache.CacheHandler
{
    sealed class LocalErrors : CommonErrors
    {
        internal static Exception NoValidCacheStoreFound ()
        {
            return new ArgumentException(FormatMessage(CacheResources.NoValidCacheStoreFound));
        }
    }
}
