namespace Services.Integration.Wcf
{    
    public interface IExternalWcfServiceConfiguration
    {
        string BaseUri { get; set; }
        string Endpoint { get; set; }
        string Dns { get; set; }
        int CloseTimeout { get; set; }
        int OpenTimeout { get; set; }
        int SendTimeout { get; set; }
        int ReceiveTimeout { get; set; }
        bool EnableRequestResponseLogging { get; set; }
        bool SecureLogs { get; set; }
        IExternalWcfServiceAuthenticationConfiguration AuthenticationConfig { get; set; }
    }
}
