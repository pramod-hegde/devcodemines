namespace Services.Core.Contracts
{
    public enum DependentComponentTypes
    {
        None = 0,
        Disk = 1,
        Server = 2,
        WebService = 4,
        WindowsService = 8,
        WcfService = 16,
        WebApi = 32,
        GatewayService = 64,
        SqlServer = 128,
        RedisCache = 256,
        AzureSql = 512,
        CosmosDb = 1024,
        WindowsContainer = 2048,
        LinuxContainer = 4096,
        AzureServiceBus = 5000,
        AzureFunctions = 6000,
        NoAuthEndpoint = 7000
    }
}
