using System.Threading.Tasks;

namespace Services.Integration.Core.Workflow
{
    public interface IWorkflowFactory
    {
        IWorkflowBuilder DefaultBuilder { get; }
        Task<IWorkflowResult> Execute<T, TConfig>(T input, TConfig configuration);
    }
}