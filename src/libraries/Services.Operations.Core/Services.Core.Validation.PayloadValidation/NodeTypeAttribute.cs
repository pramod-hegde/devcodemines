using System;

namespace Services.Core.Validation.PayloadValidation
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple =false)]
    class NodeTypeAttribute : Attribute
    {
        public ValidationActions Action { get; private set; }

        public NodeTypeAttribute(ValidationActions action)
        {
            Action = action;
        }
    }
}
