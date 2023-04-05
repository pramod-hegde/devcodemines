using System.Threading.Tasks;

namespace Services.Integration.Core.Workflow
{
    public interface IFunctionalModule
    {
        Task<IWorkflowResult> Process<T, TConfig> (T input, TConfig configuration);
    }
}
