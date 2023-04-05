using Services.Core.Logging;
using Services.Integration.Core;

namespace Services.Integration.Sql
{
    public delegate IExternalIntegrationMetadata ExternalSqlMetadataHandler(string dependentServiceAction);    
    public interface ISqlActionConfig
    {
        ILogger Logger { get; set; }        
        ExternalSqlMetadataHandler OnServiceInvoke { get; set; }        
    }
}
