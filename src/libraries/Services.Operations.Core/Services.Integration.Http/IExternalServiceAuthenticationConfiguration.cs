using System.Threading.Tasks;

namespace Services.Integration.Http
{
    public delegate Task<object> ExternalServiceAuthCallback(params object[] arguments);

    public interface IExternalServiceAuthenticationConfiguration
    {
        object Configuration { get; set; }
        ExternalServiceAuthCallback AuthenticationCallback { get; }
    }
}
