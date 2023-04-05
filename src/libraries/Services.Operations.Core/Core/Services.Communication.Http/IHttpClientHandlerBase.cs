using System.Net.Http;

namespace Services.Communication.Http
{
    internal interface IHttpClientHandlerBase
    {
        HttpClient GetClient ();
    }
}
