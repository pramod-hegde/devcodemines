using Services.Core.Logging;
using Services.Integration.Core;
using System.Collections.Generic;

namespace Services.Integration.Wcf
{
    public delegate IExternalWcfClient ExternalServiceClientCreationHandler(IExternalWcfServiceConfiguration metadata, IDictionary<string, string> additionalHeaderProperties = null);
    public delegate IExternalIntegrationMetadata ExternalServiceMetadataHandler(string dependentServiceAction);

    public interface IWcfActionConfig
    {
        string TrackingId { get; set; }
        ILogger Logger { get; set; }
        ExternalServiceClientCreationHandler OnExternalServiceClientCreate { get; set; }
        ExternalServiceMetadataHandler OnServiceInvoke { get; set; }
    }   
}
