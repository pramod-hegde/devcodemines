using System;
using System.Collections.Generic;
using System.Text;
using Services.Core.Contracts;

namespace Services.Data.CosmosDb
{
    class CosmosDbSourceAdapterInstanceConfiguration<TCache> : ICosmosDbAccessAdapterInstanceConfiguration<TCache>
    {
        public CosmosDbConnection Connection { get; set; }
        public string Collection { get; set; }
        public int? Retries { get; set; }
        public TimeSpan? RetryInterval { get; set; }
        public TCache CacheManager { get; set; }
        public bool UseReadOnlyConnection { get; set; }
        public IList<string> PreferredReadLocations { get; set; }
        public IEnumerable<ICosmosDbHandler> Handlers { get; set; }
    }   
}
