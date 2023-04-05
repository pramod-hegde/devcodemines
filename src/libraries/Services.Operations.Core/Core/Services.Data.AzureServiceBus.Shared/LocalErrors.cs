using Services.Core.Common;
using Services.Data.AzureServiceBus.Shared;
using Services.Data.Common.Exceptions;
using System;

namespace Services.Data.AzureServiceBus
{
    public sealed class LocalErrors : CommonErrors
    {
        public static Exception ConnectionStringMissing ()
        {
            return new ArgumentException(FormatMessage(AsbResources.ConnectionStringMissing));
        }

        public static Exception ClosedTopicClient ()
        {
            return new ArgumentException(FormatMessage(AsbResources.TopicClientIsInClosedState));
        }

        public static Exception CouldNotCreateTopicClientWithSettings ()
        {
            return new TopicClientException(FormatMessage(AsbResources.CouldNotCreateTopicClientWithSettings));
        }

        public static Exception CannotCastBodyToGeneric<T> ()
        {
            return new TypeLoadException(FormatMessage(AsbResources.CannotCastServiceBusMessageBodyToType, typeof(T).Name));
        }
    }
}
