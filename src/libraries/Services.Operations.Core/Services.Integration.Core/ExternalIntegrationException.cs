using System;
using System.Runtime.Serialization;

namespace Services.Integration.Core
{
    public class ExternalIntegrationException : Exception
    {
        public ExternalIntegrationException()
        {
        }

        public ExternalIntegrationException(string message) : base(message)
        {
        }

        public ExternalIntegrationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ExternalIntegrationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
