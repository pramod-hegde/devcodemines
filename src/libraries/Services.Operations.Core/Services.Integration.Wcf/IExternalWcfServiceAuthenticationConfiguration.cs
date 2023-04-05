using System.Threading.Tasks;

namespace Services.Integration.Wcf
{
    public delegate Task<object> ExternalServiceAuthCallback();

    public interface IExternalWcfServiceAuthenticationConfiguration
    {
        object Configuration { get; set; }
        ExternalServiceAuthCallback AuthenticationCallback { get; }
    }
}
