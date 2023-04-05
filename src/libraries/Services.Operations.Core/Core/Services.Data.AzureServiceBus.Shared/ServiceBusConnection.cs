namespace Services.Data.AzureServiceBus
{
    public class ServiceBusConnection 
    {
        public string Namespace { get; set; }
        public bool UseManagedIdentity { get; set; }
        public string ConnectionString { get; set; }
    }
}
