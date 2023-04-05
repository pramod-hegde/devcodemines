namespace Services.Integration.Blob
{
    public interface IBlobConfiguration
    {
        bool EnableRequestResponseLogging { get; set; }
        bool CompressWhileStore { get; set; }
        BlobOperationTypes OperationType { get; set; }
        string ContainerFormat { get; set; }
        string BlobFormat { get; set; }
        IBlobAuthenticationConfiguration AuthenticationConfig { get; set; }
    }
}
