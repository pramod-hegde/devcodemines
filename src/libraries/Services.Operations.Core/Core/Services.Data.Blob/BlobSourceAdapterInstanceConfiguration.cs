using System;
using System.Collections.Generic;
using Services.Core.Contracts;

namespace Services.Data.Blob
{
    class BlobSourceAdapterInstanceConfiguration<TCache> : IBlobAccessAdapterInstanceConfiguration<TCache>
    {
        public BlobConnection Connection { get; set; }        
        public int? Retries { get; set; }
        public TimeSpan? RetryInterval { get; set; }
        public TCache CacheManager { get; set; }
        public IEnumerable<IBlobHandler> BlobHandlers { get; set; }
    }   
}
