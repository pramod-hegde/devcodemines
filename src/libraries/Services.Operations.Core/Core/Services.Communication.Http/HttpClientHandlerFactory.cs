using Services.Core.Contracts;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Services.Communication.Http
{
    sealed class HttpClientHandlerFactory
    {
        internal static async Task<IHttpClientHandlerBase> GetClientHandler<TCache>(IHttpClientConfig config, TCache cache = default)
        {
            var attr = await GetAuthAttribute(config);
            if (attr is X509Certificate2 certificate)
            {
                return new CertificateClientHandler<TCache>(config, certificate, cache);
            }
            else if (attr is OAuthClientType oauth)
            {
                return new OAuthClientHandler<TCache>(config, oauth, cache);
            }
            else if (attr is NoAuthType noauth)
            {
                return new NoAuthClientHandler<TCache>(config, noauth, cache);
            }
            else
            {
                throw new NotSupportedException($"The authentication type {attr.GetType()} is not supported");
            }
        }

        private static async Task<object> GetAuthAttribute(IHttpClientConfig config)
        {
            if (config.AuthenticationCallback == null)
            {
                return new NoAuthType();
            }

            var auth = await config.AuthenticationCallback();
            if (auth == null)
            {
                throw new ArgumentNullException($"Authentication cannot be null");
            }

            if (auth is string @string)
            {
                return new OAuthClientType { BearerToken = @string };
            }

            else if (auth is X509Certificate2)
            {
                return auth;
            }

            return new object();
        }
    }
}
