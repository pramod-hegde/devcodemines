namespace Services.Integration.Core
{
    public enum ExternalIntegrationTypes
    {
        Http = 0,
        Sql = 1 << 0,
        Blob = 1 << 1,
        AzureServiceBus = 1 << 2,
        CosmosDb = 1 << 3,
        Wcf = 1 << 4
    }
}
