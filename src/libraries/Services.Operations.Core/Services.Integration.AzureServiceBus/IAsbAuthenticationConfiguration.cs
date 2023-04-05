using Services.Data.AzureServiceBus;
using System.Threading.Tasks;

namespace Services.Integration.AzureServiceBus
{
    public delegate Task<object> AsbMessageEncryptionAuthCallback(params object[] args);

    public delegate ServiceBusConnection AsbConnectionAuthCallback(bool secondaryConnection = false);

    public interface IAsbAuthenticationConfiguration
    {
        object Configuration { get; set; }
        object SecondaryConfiguration { get; set; }
        AsbConnectionAuthCallback AuthenticationCallback { get; }
        AsbMessageEncryptionAuthCallback MessageEncryptionAuthCallback { get; }
    }
}
