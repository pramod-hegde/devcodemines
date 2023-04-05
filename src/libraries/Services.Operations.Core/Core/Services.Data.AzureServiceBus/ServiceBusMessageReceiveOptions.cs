namespace Services.Data.AzureServiceBus
{
    public class ServiceBusMessageReceiveOptions
    {
        public ServiceBusMessageReceiveMode Mode { get; set; }
        public int MaxMessages { get; set; }
    }
}
