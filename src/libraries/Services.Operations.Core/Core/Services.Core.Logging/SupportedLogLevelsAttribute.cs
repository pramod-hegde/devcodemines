using System;

namespace Services.Core.Logging
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SupportedLogLevelsAttribute : Attribute
    {
        public LogLevel[] SupportedLogLevels { get; }

        public SupportedLogLevelsAttribute(params LogLevel[] loglevels) => SupportedLogLevels = loglevels ?? throw new ArgumentNullException(nameof(loglevels));
    }
}