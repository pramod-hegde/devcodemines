namespace Services.Integration.Wcf
{
    public interface IExternalWcfClient
    {
        void Close();
        void Configure(IExternalWcfServiceConfiguration configuration);
    }    
}
