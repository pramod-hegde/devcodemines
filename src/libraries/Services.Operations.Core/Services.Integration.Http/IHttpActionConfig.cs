using Services.Integration.Core;
using Services.Core.Contracts;
using System.Collections.Generic;
using Services.Core.Logging;

namespace Services.Integration.Http
{
    public delegate ICommunicationClient ExternalServiceClientCreationHandler(IExternalServiceConfiguration metadata, IDictionary<string, string> additionalHeaderProperties = null);
    public delegate IExternalIntegrationMetadata ExternalServiceMetadataHandler(string dependentServiceAction);

    public interface IHttpActionConfig
    {
        string TrackingId { get; set; }
        ILogger Logger { get; set; }
        ExternalServiceClientCreationHandler OnExternalServiceClientCreate { get; set; }
        ExternalServiceMetadataHandler OnServiceInvoke { get; set; }
    }   
}
