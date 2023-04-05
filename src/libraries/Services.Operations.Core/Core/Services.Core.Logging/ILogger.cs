using System;
using System.Collections.Generic;

namespace Services.Core.Logging
{
    public interface ILogger
    {
        void AddScope(ILoggerContext logContext = null);
        void Log<TLog>(LogLevel logLevel, TLog logValue, IDictionary<string, object> additionalProperties = null, Func<TLog, Exception, string> formatter = null);
        void Dispose();
    }
}