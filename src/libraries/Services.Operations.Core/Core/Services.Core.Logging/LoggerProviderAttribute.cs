using System;

namespace Services.Core.Logging
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class LoggerProviderAttribute : Attribute
    {
        public string ProviderName { get; }

        public LoggerProviderAttribute(string providerName) => ProviderName = providerName ?? throw new ArgumentNullException(nameof(providerName));
    }
}