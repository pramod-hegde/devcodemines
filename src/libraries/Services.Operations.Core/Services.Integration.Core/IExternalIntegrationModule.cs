using System.Threading.Tasks;

namespace Services.Integration.Core
{
    public interface IExternalIntegrationModule
    {
        Task<object> ExecuteAsync<TConfig, TIn>(TIn input, TConfig config, string externalAction);
    }
}
