using System.Net;
using System.Runtime.Serialization;

namespace Services.Communication.Http
{
    /// <summary>
    /// Used for retriable errors
    /// </summary>
    public class RetriableHttpException : HttpListenerException
    {
        public RetriableHttpException()
        {
        }

        public RetriableHttpException(int errorCode) : base(errorCode)
        {
        }

        public RetriableHttpException(int errorCode, string message) : base(errorCode, message)
        {
        }

        protected RetriableHttpException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
        }
    }
}
