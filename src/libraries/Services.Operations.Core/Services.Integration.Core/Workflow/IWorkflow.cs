namespace Services.Integration.Core.Workflow
{
    public interface IWorkflow
    {
        IWorkflowStep First ();
        IWorkflowStep Next ();
        bool IsDone { get; }
        IWorkflowStep Current { get; }
    }
}