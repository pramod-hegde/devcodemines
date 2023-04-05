using System;
using System.Runtime.Serialization;

namespace Services.Integration.Core
{
    public class RetriableException : Exception
    {
        public RetriableException()
        {
        }

        public RetriableException(string message) : base(message)
        {
        }

        public RetriableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RetriableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
