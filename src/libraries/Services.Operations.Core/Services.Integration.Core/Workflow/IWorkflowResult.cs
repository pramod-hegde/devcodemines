using System;

namespace Services.Integration.Core.Workflow
{
    public interface IWorkflowResult
    {
        bool Success { get; set; }
        bool Exit { get; set; }
        string Message { get; set; }
        Exception Exception { get; set; }
        object Result { get; set; }
    }
}
