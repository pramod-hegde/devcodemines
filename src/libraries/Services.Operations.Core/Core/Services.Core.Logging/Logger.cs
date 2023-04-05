using System;
using System.Collections.Generic;
using System.Reflection;

namespace Services.Core.Logging
{
    public class Logger<T> : ILogger<T>
    {
        private readonly ILoggerFactory _loggerFactory = default;
        private IConcreateLoggerProvider _loggerProvider = default;

        public Logger(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        private string GetContext(Type type)
        {
            if (type.GetTypeInfo().IsGenericType)
            {
                var fullName = type.GetGenericTypeDefinition().FullName;

                var parts = fullName.Split('+');

                for (var i = 0; i < parts.Length; i++)
                {
                    var partName = parts[i];

                    var backTickIndex = partName.IndexOf('`');
                    if (backTickIndex >= 0)
                    {
                        partName = partName.Substring(0, backTickIndex);
                    }

                    parts[i] = partName;
                }

                return string.Join(".", parts);
            }
            else
            {
                var fullName = type.FullName;

                if (type.IsNested)
                {
                    fullName = fullName.Replace('+', '.');
                }

                return fullName;
            }
        }

        void ILogger.AddScope(ILoggerContext logContext)
        {
            foreach (var l in _loggerProvider?.Loggers)
            {
                l?.AddScope(logContext);
            }
        }

        void ILogger.Log<TLog>(LogLevel logLevel, TLog logValue, IDictionary<string, object> additionalProperties = null, Func<TLog, Exception, string> formatter = null)
        {
            foreach (var l in _loggerProvider?.Loggers)
            {
                l?.Log(logLevel, logValue, additionalProperties, formatter);
            }
        }

        void ILogger<T>.Setup()
        {
            _loggerProvider = _loggerFactory.DefaultBuilder.Build();
        }

        ILogger<T> ILogger<T>.AddProvider<TConfig>(IProviderConfiguration<TConfig> configuration)
        {
            _loggerFactory?.DefaultBuilder.Add(configuration);
            return this;
        }

        void ILogger.Dispose()
        {
            foreach (var l in _loggerProvider?.Loggers)
            {
                l?.Dispose();
            }
        }
    }
}