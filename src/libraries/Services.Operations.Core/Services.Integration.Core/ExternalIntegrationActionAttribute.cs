using System;

namespace Services.Integration.Core
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class ExternalIntegrationActionAttribute : Attribute
    {
        public string Action { get; private set; }
        public ExternalIntegrationActionAttribute(string action) : base()
        {
            Action = action;
        }
    }
}
