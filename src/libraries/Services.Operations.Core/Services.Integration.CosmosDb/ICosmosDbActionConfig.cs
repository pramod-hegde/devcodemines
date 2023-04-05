using Microsoft.Extensions.Caching.Memory;
using Services.Core.Logging;
using Services.Data.CosmosDb;
using Services.Integration.Core;

namespace Services.Integration.CosmosDb
{
    public delegate IExternalIntegrationMetadata CosmosDbActionMetadataHandler(string dependentServiceAction);

    public interface ICosmosDbActionConfig
    {
        ILogger Logger { get; set; }
        IMemoryCache Cache { get; set; }
        ICosmosDbHandler StorageHandler { get; set; }
        CosmosDbActionMetadataHandler OnActionInvoke { get; set; }
    }
}
