using Services.Core.Contracts;
using System;
using System.Collections.Generic;

namespace Services.Core.Monitoring
{
    public interface IHttpServiceMonitorConfig
    {
        string Identifier { get; set; }
        string Url { get; set; }
        AuthenticationCallback AuthenticationCallback { get; set; }
        TimeSpan ConnectionTimeout { get; set; }
        string HttpMethod { get; set; }
        IDictionary<string, string> HeaderProperties { get; set; }
        object Data { get; set; }
    }
}
