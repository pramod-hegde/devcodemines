using System.Net;
using System.Runtime.Serialization;

namespace Services.Communication.Http
{
    /// <summary>
    /// Abstracts Http response code 204
    /// </summary>
    public class NoHttpContentException : HttpListenerException
    {
        public NoHttpContentException()
        {
        }

        public NoHttpContentException(string message) : base((int)HttpStatusCode.NoContent, message)
        {
        }

        public NoHttpContentException(int errorCode) : base(errorCode)
        {
        }

        public NoHttpContentException(int errorCode, string message) : base(errorCode, message)
        {
        }

        protected NoHttpContentException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
        }
    }
}
