using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Core.Logging
{
    sealed class LoggerBuilder : ILoggerBuilder
    {
        private List<ILoggerProvider> _providers = new List<ILoggerProvider>();
        private List<Type> _registeredLoggerProviders = new List<Type>();

        IConcreateLoggerProvider ILoggerBuilder.Build()
        {
            return new ConcreateLoggerProvider(this);
        }

        ILoggerBuilder ILoggerBuilder.Add<TConfig>(IProviderConfiguration<TConfig> providerConfig)
        {
            _providers.Add(GetProvider(providerConfig));
            return this;
        }

        private ILoggerProvider GetProvider<TConfig>(IProviderConfiguration<TConfig> providerConfig)
        {
            if (!_registeredLoggerProviders.Any())
            {
                var type = typeof(ILoggerProvider);
                var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(p => type.IsAssignableFrom(p));

                if (types.Any())
                {
                    _registeredLoggerProviders.AddRange(types.ToList());
                }
            }

            foreach (var m in _registeredLoggerProviders)
            {
                var a = m.CustomAttributes.FirstOrDefault(t => t.AttributeType == typeof(LoggerProviderAttribute));

                if (a == null)
                {
                    continue;
                }

                if (a.ConstructorArguments[0].Value.ToString() == providerConfig.ProviderName)
                {
                    return (ILoggerProvider)Activator.CreateInstance(m, providerConfig.Configuration);
                }
            }

            return null;
        }

        internal int Count => _providers.Count;

        internal ILogger this[int index] => _providers[index].CreateLogger();
    }
}