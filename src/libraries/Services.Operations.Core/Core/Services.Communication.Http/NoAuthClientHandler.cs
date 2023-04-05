using System.Net.Http;

namespace Services.Communication.Http
{
    sealed class NoAuthClientHandler<TCache> : HttpClientHandlerBase<NoAuthType, TCache>
    {
        internal NoAuthClientHandler(IHttpClientConfig config, NoAuthType authResponse, TCache cacheManager) : base(config, authResponse, cacheManager)
        {

        }

        internal override bool NeedsHttpHandler => false;

        internal override HttpClientHandler WebHandler => null;

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
