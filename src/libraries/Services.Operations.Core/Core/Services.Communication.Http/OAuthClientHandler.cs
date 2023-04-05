using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Services.Communication.Http
{
    sealed class OAuthClientHandler<TCache> : HttpClientHandlerBase<OAuthClientType, TCache>
    {
        internal OAuthClientHandler(IHttpClientConfig config, OAuthClientType authResponse, TCache cacheManager) : base(config, authResponse, cacheManager) { }

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

                if (_authResponse == null || string.IsNullOrWhiteSpace(_authResponse.BearerToken))
                {
                    throw new ArgumentException("OAuth token is either null or empty");
                }

                if (!_client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authResponse.BearerToken);
                }
            }
        }
    }
}
