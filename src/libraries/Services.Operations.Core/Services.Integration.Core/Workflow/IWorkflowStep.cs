using System.Threading.Tasks;

namespace Services.Integration.Core.Workflow
{
    public interface IWorkflowStep
    {
        Task<IWorkflowResult> ExecuteAsync<T, TConfig> (T input, TConfig config);
    }
}
