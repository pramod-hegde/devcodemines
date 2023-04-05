using Services.Data.AzureServiceBus;
using Services.Data.Common;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace Services.Integration.AzureServiceBus
{
    class ServiceBusMessageAdapterConfig : IAzureServiceBusAccessAdapterConfiguration<IMemoryCache>
    {
        public string Id { get; }
        public ImmutableMessageTargetTypes TargetType { get; }
        public string Topic { get; }
        public string Subscription { get; }
        public string Queue { get; }
        public AzureStorageLocationMode? LocationMode { get; }
        public ServiceBusConnection Connection { get; set; }
        public int? Retries { get; set; }
        public TimeSpan? RetryInterval { get; set; }
        public IMemoryCache CacheManager { get; set; }
        public IEnumerable<IMessagingHandler> MessageHandlers { get; set; }
        public bool EnableSharding { get; set; }
        public int MinimumPartitionIndex { get; set; }
        public int MaximumPartitionIndex { get; set; }

        public ServiceBusMessageAdapterConfig(IAsbConfiguration settings, IMemoryCache cache = null, string id = "DefaultMessageAdapterConfig", bool secondaryConnection = false)
        {
            Id = id;
            Connection = settings.AuthenticationConfig.AuthenticationCallback(secondaryConnection);
            CacheManager = cache;
            TargetType = (ImmutableMessageTargetTypes)Enum.Parse(typeof(ImmutableMessageTargetTypes), settings.ChannelType.ToString());

            if (TargetType == ImmutableMessageTargetTypes.Queue)
            {
                Queue = settings.ChannelIdentifier;
            }
            else
            {
                var channelIdentifierSplit = settings.ChannelIdentifier.Split(':');
                Topic = channelIdentifierSplit[0];
            }

            if (settings.EnableSharding) 
            {
                EnableSharding = settings.EnableSharding;
                MinimumPartitionIndex = settings.MinimumPartitionIndex;
                MaximumPartitionIndex = settings.MaximumPartitionIndex;
            }
        }
    }
}
