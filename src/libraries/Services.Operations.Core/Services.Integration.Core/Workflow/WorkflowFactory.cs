using System.Threading.Tasks;

namespace Services.Integration.Core.Workflow
{
    public sealed class WorkflowFactory : IWorkflowFactory
    {
        public string Id => "WorkflowFactory";

        public WorkflowFactory ()
        {
            if (_workflowBuilder == null)
            {
                _workflowBuilder = new WorkflowBuilder();
            }
        }

        readonly IWorkflowBuilder _workflowBuilder;
        IWorkflow _workflow;
        IWorkflowBuilder IWorkflowFactory.DefaultBuilder => _workflowBuilder;

        async Task<IWorkflowResult> IWorkflowFactory.Execute<T, TConfig> (T input, TConfig configuration)
        {
            _workflow = _workflowBuilder?.Build();

            if (_workflow == null || _workflow.IsDone)
            {
                return default;
            }

            IWorkflowResult result = default;

            while (_workflow.Next() != null)
            {
                result = await _workflow.Current.ExecuteAsync(result == default ? input : result.Result, configuration);
                if (result == null || !result.Success || result.Exit)
                {
                    return result;
                }
            }
            return result;
        }
    }
}