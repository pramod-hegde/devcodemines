using Services.Data.Common;
using System;
using System.Collections.Generic;

namespace Services.Data.AzureServiceBus
{
    public sealed class AzureServiceBusSourceAdapterInstanceConfiguration<TCache> : IAzureServiceBusAccessAdapterInstanceConfiguration<TCache>
    {
        public string Id { get; set; }

        public ImmutableMessageTargetTypes TargetType { get; set; }

        public string Topic { get; set; }

        public string Subscription { get; set; }

        public string Queue { get; set; }        

        public AzureStorageLocationMode? LocationMode { get; set; }

        public TCache CacheManager { get; set; }        

        public int? Retries { get; set; }

        public TimeSpan? RetryInterval { get; set; }
        public IEnumerable<IMessagingHandler> MessageHandlers { get; set; }
        public ServiceBusConnection Connection { get; set; }
        public bool EnableSharding { get; set; }
        public int MinimumPartitionIndex { get; set; }
        public int MaximumPartitionIndex { get; set; }
    }
}
