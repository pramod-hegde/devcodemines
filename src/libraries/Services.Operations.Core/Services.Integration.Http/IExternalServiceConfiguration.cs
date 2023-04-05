namespace Services.Integration.Http
{    
    public interface IExternalServiceConfiguration
    {
        string BaseUri { get; set; }
        string Endpoint { get; set; }
        bool EnableRequestResponseLogging { get; set; }
        IExternalServiceAuthenticationConfiguration AuthenticationConfig { get; set; }
        IExternalServiceAuthenticationConfiguration MessageEncryptionConfig { get; set; }
    }
}
