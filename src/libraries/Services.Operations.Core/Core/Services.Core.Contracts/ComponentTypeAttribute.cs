using System;
using System.ComponentModel;

namespace Services.Core.Contracts
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ComponentTypeAttribute : Attribute
    {
        public DependentComponentTypes Type { get; } = DependentComponentTypes.None;
        public ComponentTypeAttribute (DependentComponentTypes type)
        {
            Type = type;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals (object obj)
        {
            return base.Equals(obj);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode ()
        {
            return base.GetHashCode();
        }
    }
}
