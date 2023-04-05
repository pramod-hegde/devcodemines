using System;
using System.Runtime.Serialization;

namespace Services.Integration.Core
{
    public class RetriableExternalIntegrationException : Exception
    {
        public RetriableExternalIntegrationException()
        {
        }

        public RetriableExternalIntegrationException(string message) : base(message)
        {
        }

        public RetriableExternalIntegrationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RetriableExternalIntegrationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
