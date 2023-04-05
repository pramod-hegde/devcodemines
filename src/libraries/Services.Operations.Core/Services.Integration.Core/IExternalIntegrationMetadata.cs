namespace Services.Integration.Core
{
    public interface IExternalIntegrationMetadata
    {
        ExternalIntegrationTypes IntegrationType { get; set; }
        string ServiceType { get; set; }
        object Settings { get; set; }
    }
}
