using System;
using System.Linq.Expressions;

namespace Services.Core.FaultHandling.Shared
{
    sealed class ConditionalContext
    {
        internal Delegate Condition { get; set; }
        internal Delegate Action { get; set; }
        internal Condition ConditionType { get; set; }
    }
}