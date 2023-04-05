using System;
using System.Collections.Generic;

namespace Services.Data.AzureServiceBus
{
    public interface IAzureServiceBusMessageWriterSetting
    {
        IDictionary<string, object> MessageProperties { get; }
        string PartitionKey { get; }
        string CorrelationId { get; }
        string MessageId { get; }
        string SessionId { get; }
        string ContentType { get; }
        bool EnableDelayedMessaging { get; set; }
        DateTime ScheduledEnqueueTime { get; set; }
    }
}
