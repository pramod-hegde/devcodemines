using Services.Data.Common;

namespace Services.Data.AzureServiceBus
{
    public interface IAzureServiceBusAdapterConfiguration<TCache> : IDataAccessAdapterInstanceConfiguration<ServiceBusConnection, TCache>
    {      
        AzureStorageLocationMode? LocationMode { get; }
        bool EnableSharding { get; set; }
        int MinimumPartitionIndex { get; set; }
        int MaximumPartitionIndex { get; set; }
    }
}
