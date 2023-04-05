using System;
using System.Linq;

namespace Services.Integration.Core
{
    public sealed class ModuleFactory<TModuleConfiguration> : IServiceModuleFactory<TModuleConfiguration>
    {
        IServiceModule<TModuleConfiguration> IServiceModuleFactory<TModuleConfiguration>.GetServiceModule<TValue>(TModuleConfiguration moduleConfiguration, TValue moduleIdentifier)
        {
            var type = typeof(IServiceModule<TModuleConfiguration>);
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(p => type.IsAssignableFrom(p));

            foreach (var m in types)
            {
                var a = (ModuleAttribute)m.GetCustomAttributes(typeof(ModuleAttribute), false).FirstOrDefault();

                if (a == null)
                {
                    continue;
                }

                if (a.ModuleIdentifier.Equals(moduleIdentifier))
                {
                    return (IServiceModule<TModuleConfiguration>)Activator.CreateInstance(m, moduleConfiguration);
                }
            }

            return default;
        }
    }
}
