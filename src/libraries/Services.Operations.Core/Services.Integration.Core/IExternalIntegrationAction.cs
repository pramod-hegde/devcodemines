using System.Threading.Tasks;

namespace Services.Integration.Core
{
    public interface IExternalIntegrationAction
    {
        Task<object> ExecuteAsync<TIn, TConfig>(TIn input, TConfig config);
    }
}
