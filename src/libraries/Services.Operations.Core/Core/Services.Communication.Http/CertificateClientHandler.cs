using Services.Core.Contracts;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace Services.Communication.Http
{
    sealed class CertificateClientHandler<TCache> : HttpClientHandlerBase<X509Certificate2, TCache>
    {
        internal CertificateClientHandler(IHttpClientConfig config, X509Certificate2 authResponse, TCache cacheManager) : base(config, authResponse, cacheManager) { }

        internal override bool NeedsHttpHandler => true;

        internal override HttpClientHandler WebHandler => GetInternalHandler();

        private HttpClientHandler GetInternalHandler()
        {
            lock (_mutext)
            {
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual
                };
                clientHandler.ClientCertificates.Add(_authResponse);
                return clientHandler;
            }
        }

        public override HttpClient GetClient()
        {
            lock (_mutext)
            {
                CreateClient();
            }

            return _client;
        }

        internal override void Decorate()
        {
            lock (_mutext)
            {
                base.Decorate();
            }
        }
    }
}
