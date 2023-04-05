using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Services.Communication.Http
{
    /// <summary>
    /// Abstracts all successful http response codes other than 200, 204 and 206
    /// </summary>
    public class HttpStatusCodeException : HttpListenerException
    {
        public HttpResponseMessage ResponseMessage { get; set; }

        public HttpStatusCodeException()
        {
        }

        public HttpStatusCodeException(int errorCode) : base(errorCode)
        {
        }

        public HttpStatusCodeException(string message, HttpResponseMessage responseMessage) : base((int)responseMessage.StatusCode, $"{message}. {responseMessage.ReasonPhrase}")
        {
            ResponseMessage = responseMessage;
            this.Data.Add("Message", message);
            this.Data.Add("Response.StatusCode", responseMessage.StatusCode);
            this.Data.Add("Response.ReasonPhrase", responseMessage.ReasonPhrase);
            this.Data.Add("Response.Content", responseMessage.Content);
            this.Data.Add("Request.RequestUri", responseMessage.RequestMessage.RequestUri);
            this.Data.Add("Request.Headers", JsonConvert.SerializeObject(responseMessage.RequestMessage.Headers));
        }

        public HttpStatusCodeException(HttpResponseMessage responseMessage) : base((int)responseMessage.StatusCode, responseMessage.ReasonPhrase)
        {
            ResponseMessage = responseMessage;            
            this.Data.Add("Response.StatusCode", responseMessage.StatusCode);
            this.Data.Add("Response.ReasonPhrase", responseMessage.ReasonPhrase);
            this.Data.Add("Response.Content", responseMessage.Content);
            this.Data.Add("Request.RequestUri", responseMessage.RequestMessage.RequestUri);
            this.Data.Add("Request.Headers", JsonConvert.SerializeObject(responseMessage.RequestMessage.Headers));
        }

        public HttpStatusCodeException(int errorCode, string message) : base(errorCode, message)
        {
        }

        protected HttpStatusCodeException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
        }
    }
}
