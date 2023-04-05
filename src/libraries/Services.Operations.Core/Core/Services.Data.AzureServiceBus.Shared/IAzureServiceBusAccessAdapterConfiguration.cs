using System.Collections.Generic;

namespace Services.Data.AzureServiceBus
{
    public interface IAzureServiceBusAccessAdapterConfiguration<TCache> : IAzureServiceBusAdapterConfiguration<TCache>
    {
        string Id { get; }

        ImmutableMessageTargetTypes TargetType { get; }

        string Topic { get; }

        string Subscription { get; }

        string Queue { get; }
        IEnumerable<IMessagingHandler> MessageHandlers { get; set; }
    }
}
