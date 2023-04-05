namespace Services.Integration.Core.Workflow
{
    public interface IWorkflowBuilder
    {
        IWorkflowBuilder AddStep (IWorkflowStep step);
        IWorkflow Build ();
    }
}