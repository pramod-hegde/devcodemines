using Services.Data.AzureServiceBus;
using System;
using System.Collections.Generic;

namespace Services.Integration.AzureServiceBus
{
    public class WriterSetting : IAzureServiceBusMessageWriterSetting
    {
        public IDictionary<string, object> MessageProperties { get; }

        public string PartitionKey { get; }

        public string CorrelationId { get; }

        public string MessageId { get; }

        public string SessionId { get; }

        public string ContentType => "text";

        public bool EnableDelayedMessaging { get; set; }
        public DateTime ScheduledEnqueueTime { get; set; }
        public WriterSetting(Dictionary<string, object> additionalMessageProperties = null, string partitionKey = "", string correlationId = "", string messageId = "", string sessionId = "")
        {
            MessageProperties = additionalMessageProperties;
            PartitionKey = partitionKey;
            CorrelationId = correlationId;
            MessageId = messageId;
            SessionId = sessionId;
        }
    }
}
