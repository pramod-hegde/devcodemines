using System;

namespace Services.Integration.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ModuleAttribute : Attribute
    {
        public object ModuleIdentifier { get; }

        public ModuleAttribute(object value)
        //ModuleAttribute<T> is simpler to use. C# doesn't support generic attribute types. ETA C# 8.0 onwards. If supported by the language, change this.
        {
            ModuleIdentifier = value;
        }
    }
}
