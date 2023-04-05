using System.Collections.Generic;

namespace Services.Integration.AzureServiceBus
{
    public interface IAsbConfiguration
    {
        bool EnableRequestResponseLogging { get; set; }
        AsbOperationTypes OperationType { get; set; }
        AsbChannelTypes ChannelType { get; set; }
        string ChannelIdentifier { get; set; }
        IAsbAuthenticationConfiguration AuthenticationConfig { get; set; }
        IAsbAuthenticationConfiguration MessageEncryptionConfig { get; set; }
        IDictionary<string, object> AdditionalProperties { get; set; }
        bool EnableSharding { get; set; }
        int MinimumPartitionIndex { get; set; }
        int MaximumPartitionIndex { get; set; }
    }
}
