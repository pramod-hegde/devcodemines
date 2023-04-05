using Microsoft.Extensions.Caching.Memory;
using Services.Data.CosmosDb;

namespace Services.Integration.CosmosDb
{
    public delegate ICosmosDbAccessAdapterInstanceConfiguration<IMemoryCache> CosmosDbStorageHandlerAuthCallback();
    public interface ICosmosDbAuthenticationConfiguration
    {
        object Configuration { get; set; }
        CosmosDbStorageHandlerAuthCallback AuthenticationCallback { get; }
    }
}
