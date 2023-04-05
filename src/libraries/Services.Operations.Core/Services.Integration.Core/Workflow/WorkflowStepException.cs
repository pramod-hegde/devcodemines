using System;
using System.Runtime.Serialization;

namespace Services.Integration.Core.Workflow
{
    public class WorkflowStepException : Exception
    {
        public WorkflowStepException()
        {
        }

        public WorkflowStepException(string message) : base(message)
        {
        }

        public WorkflowStepException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WorkflowStepException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}