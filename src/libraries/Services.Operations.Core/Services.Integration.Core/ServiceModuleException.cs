using System;
using System.Runtime.Serialization;

namespace Services.Integration.Core
{
    public class ServiceModuleException : Exception
    {
        public ServiceModuleException()
        {
        }

        public ServiceModuleException(string message) : base(message)
        {
        }

        public ServiceModuleException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ServiceModuleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
