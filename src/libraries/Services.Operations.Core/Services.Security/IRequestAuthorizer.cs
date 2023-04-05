using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Services.Security
{
    public interface IRequestAuthorizer
    {
        void TryValidatingHeader(HttpRequest request, out bool valid);
        Tuple<bool, string> ValidRequestorToken(HttpRequest request);
        AllowedClientHandler OnValidateClient { get; set; }
        IDictionary<string, string> AuthorizationClaims { get; }
    }
}
