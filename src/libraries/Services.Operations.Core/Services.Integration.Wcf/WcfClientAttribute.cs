using System;

namespace Services.Integration.Wcf
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class WcfClientAttribute : Attribute
    {
        public string Name { get; }

        public WcfClientAttribute(string name)
        {
            Name = name;
        }
    }
}
