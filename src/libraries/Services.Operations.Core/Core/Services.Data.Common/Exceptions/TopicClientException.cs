using System;
using System.Runtime.Serialization;

namespace Services.Data.Common.Exceptions
{
    public sealed class TopicClientException : Exception
    {
        public TopicClientException ()
        {
        }

        public TopicClientException (string message) : base(message)
        {
        }

        public TopicClientException (string message, Exception innerException) : base(message, innerException)
        {
        }

#pragma warning disable CS0628 // New protected member declared in sealed class
        protected TopicClientException (SerializationInfo info, StreamingContext context) : base(info, context)
#pragma warning restore CS0628 // New protected member declared in sealed class
        {
        }
    }
}
