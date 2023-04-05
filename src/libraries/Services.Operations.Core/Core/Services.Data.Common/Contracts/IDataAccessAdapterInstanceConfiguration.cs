using Services.Core.Contracts;
using System;

namespace Services.Data.Common
{
    public interface IDataAccessAdapterInstanceConfiguration<TConnection, TCache>
    {
        TConnection Connection { get; set; }
        int? Retries { get; set; }
        TimeSpan? RetryInterval { get; set; }
        TCache CacheManager { get; set; }
    }
}
