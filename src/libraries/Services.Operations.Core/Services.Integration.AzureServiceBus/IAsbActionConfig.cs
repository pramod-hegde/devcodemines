using Services.Integration.Core;
using Services.Data.AzureServiceBus;
using Microsoft.Extensions.Caching.Memory;
using Services.Core.Logging;

namespace Services.Integration.AzureServiceBus
{
    public delegate IExternalIntegrationMetadata AsbMetadataHandler(string dependentServiceAction);
    public interface IAsbActionConfig 
    {
        ILogger Logger { get; set; }
        IMemoryCache Cache { get; set; }
        IMessagingHandler ServiceBusMessageHandler { get; set; }
        AsbMetadataHandler OnActionInvoke { get; set; }
        string CorrelationId { get; set; }
    }
}
