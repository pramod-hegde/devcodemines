using Services.Core.Contracts;
using System;
using System.Collections.Generic;

namespace Services.Communication.Http
{
    internal interface IHttpClientConfig
    {
        string BaseUri { get; set; }
        AuthenticationCallback AuthenticationCallback { get; set; }
        TimeSpan ConnectionTimeout { get; set; }
        IDictionary<string, string> HeaderProperties { get; set; }
    }
}
