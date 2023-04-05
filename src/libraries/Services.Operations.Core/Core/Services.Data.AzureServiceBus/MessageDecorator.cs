using System;
using System.Collections.Generic;
using System.Linq;
using Azure.Messaging.ServiceBus;

namespace Services.Data.AzureServiceBus
{
    sealed class MessageDecorator
    {
        private readonly ServiceBusMessage message;

        internal MessageDecorator (ServiceBusMessage message)
        {
            this.message = message;
        }

        internal void Decorate (IAzureServiceBusMessageWriterSetting setting)
        {
            message.ContentType = setting.ContentType;
            message.CorrelationId = string.IsNullOrWhiteSpace(setting.CorrelationId) ? setting.CorrelationId : Guid.NewGuid().ToString();
            message.PartitionKey = setting.PartitionKey;
            message.MessageId = setting.MessageId;
            message.SessionId = setting.SessionId;

            if (setting.MessageProperties != null && setting.MessageProperties.Any())
            {
                AddMessageProperties(message, setting.MessageProperties);
            }

            if (setting.EnableDelayedMessaging) 
            {
                message.ScheduledEnqueueTime = setting.ScheduledEnqueueTime;
            }
        }

        private void AddMessageProperties (ServiceBusMessage message, IDictionary<string, object> messageProperties)
        {
            foreach (var p in messageProperties)
            {
                message.ApplicationProperties.Add(p);
            }
        }
    }
}