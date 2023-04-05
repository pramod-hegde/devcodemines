namespace Services.Integration.Core.Workflow
{
    sealed class Workflow : IWorkflow
    {
        private WorkflowBuilder _steps;
        private int _current = 0;
        private readonly int _step = 1;
        private bool _started;

        internal Workflow (WorkflowBuilder steps)
        {
            _steps = steps;
        }

        IWorkflowStep IWorkflow.First ()
        {
            if (!_started)
            {
                _started = true;
            }

            _current = 0;
            return _steps[_current] as IWorkflowStep;
        }

        IWorkflowStep IWorkflow.Next ()
        {
            if (!_started)
            {
                _current = 0;
                _started = true;
            }
            else
            {
                _current += _step;
            }

            if (!HasCompleted())
                return _steps[_current] as IWorkflowStep;
            else
                return null;
        }

        IWorkflowStep IWorkflow.Current => _steps[_current] as IWorkflowStep;

        bool IWorkflow.IsDone => HasCompleted();

        bool HasCompleted ()
        {
            return _current >= _steps.Count;
        }
    }
}