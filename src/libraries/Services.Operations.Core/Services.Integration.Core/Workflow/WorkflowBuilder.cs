using System.Collections;

namespace Services.Integration.Core.Workflow
{
    sealed class WorkflowBuilder : IWorkflowBuilder
    {
        private ArrayList _steps = new ArrayList();

        IWorkflow IWorkflowBuilder.Build ()
        {
            return new Workflow(this);
        }

        IWorkflowBuilder IWorkflowBuilder.AddStep(IWorkflowStep step)
        {
            _steps.Add(step);
            return this;
        }

        internal int Count => _steps.Count;

        internal object this[int index]
        {
            get => _steps[index];
            set => _steps.Add(value);
        }
    }
}