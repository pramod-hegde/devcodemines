using Services.Core.Contracts;
using System;
using System.Collections.Generic;

namespace Services.Communication.Http
{
    sealed class GenericHttpClientConfig : IHttpClientConfig
    {
        public string BaseUri { get; set; }
        public AuthenticationCallback AuthenticationCallback { get; set; }
        public TimeSpan ConnectionTimeout { get; set; }
        public IDictionary<string, string> HeaderProperties { get; set; }
    }
}
