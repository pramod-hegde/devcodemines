using System;
using System.Runtime.Serialization;

namespace Services.Communication.Http
{
    public class HttpCommunicationTimeoutException : TimeoutException
    {
        public HttpCommunicationTimeoutException()
        {
        }

        public HttpCommunicationTimeoutException(string message) : base(message)
        {
        }

        public HttpCommunicationTimeoutException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HttpCommunicationTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
