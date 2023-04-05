using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Services.Security
{
    public delegate bool AllowedClientHandler(string appIdentifier, string authDomain);

    public class DefaultRequestAuthorizer : IRequestAuthorizer
    {
        public AllowedClientHandler OnValidateClient { get; set; }

        private string _token = string.Empty;
        public IDictionary<string, string> AuthorizationClaims
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_token))
                {
                    return default;
                }

                var securityTokenHandler = new JwtSecurityTokenHandler();
                var token = securityTokenHandler.ReadJwtToken(_token);

                var claimSet = new Dictionary<string, string>();
                foreach ( var claim in token?.Claims)
                {
                    claimSet[claim.Type] = claim.Value;
                }

                return claimSet;
            }
        }

        void IRequestAuthorizer.TryValidatingHeader(HttpRequest request, out bool valid)
        {
            if (request == null || request.Headers == null)
            {
                valid = false;
            }
            else
            {
                valid = ValidateAndAddMissingFields(request);
            }
        }

        private bool ValidateAndAddMissingFields(HttpRequest request)
        {
            if (!request.Headers.Any(k => k.Key == "x-ms-correlation-id"))
            {
                request.Headers.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
            }

            return true;
        }

        Tuple<bool, string> IRequestAuthorizer.ValidRequestorToken(HttpRequest req)
        {
            if (req.HttpContext?.User?.Identity?.IsAuthenticated != true || !req.Headers.ContainsKey("Authorization"))
            {
                return new Tuple<bool, string>(false, "Unauthorized");
            }

            _token = req.Headers["Authorization"].First();

            if (string.IsNullOrWhiteSpace(_token))
            {
                return new Tuple<bool, string>(false, string.Empty);
            }

            _token = _token.Split(' ')[1];

            var securityTokenHandler = new JwtSecurityTokenHandler();
            return ValidateClient(securityTokenHandler.ReadJwtToken(_token));
        }

        private Tuple<bool, string> ValidateClient(JwtSecurityToken validatedToken)
        {
            var appId = GetAppId(validatedToken);
            var issuer = GetTokenIssuer(validatedToken);

            if (OnValidateClient != null)
            {
                return new Tuple<bool, string>(OnValidateClient(appId, issuer), appId);
            }

            return new Tuple<bool, string>(false, appId);
        }

        private string GetAppId(JwtSecurityToken token)
        {
            return token.Claims.First(claim => claim.Type == "appid").Value;
        }

        private string GetTokenIssuer(JwtSecurityToken token)
        {
            return token.Claims.First(claim => claim.Type == "iss").Value;
        }
    }
}
