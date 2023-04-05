using System.Collections.Generic;

namespace Services.Integration.CosmosDb
{
    public interface ICosmosDbConfiguration
    {
        bool EnableRequestResponseLogging { get; set; }
        CosmosDbOperations OperationType { get; set; }
        string Query { get; set; }
        bool EnableReadFromPreferredLocations { get; set; }
        string[] PreferredLocations { get; set; }
        ICosmosDbAuthenticationConfiguration AuthenticationConfig { get; set; }
        ICosmosDbAuthenticationConfiguration MessageEncryptionConfig { get; set; }
        IDictionary<string, object> AdditionalProperties { get; set; }
    }
}
