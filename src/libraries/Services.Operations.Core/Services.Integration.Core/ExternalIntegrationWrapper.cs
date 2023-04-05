using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Services.Integration.Core
{
    public sealed class ExternalIntegrationWrapper : IExternalIntegrationModule
    {
        List<Type> _registeredActionTypes = null;

        public ExternalIntegrationWrapper()
        {
            _registeredActionTypes = new List<Type>();
        }

        public string Id => nameof(ExternalIntegrationWrapper);

        async Task<object> IExternalIntegrationModule.ExecuteAsync<TConfig, TIn>(TIn input, TConfig config, string externalAction)
        {
            var action = this[externalAction];
            return await action.ExecuteAsync(input, config);
        }

        IExternalIntegrationAction this[string action]
        {
            get
            {
                if (!_registeredActionTypes.Any())
                {
                    var type = typeof(IExternalIntegrationAction);
                    var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(p => type.IsAssignableFrom(p));

                    if (types.Any())
                    {
                        _registeredActionTypes.AddRange(types.ToList());
                    }
                }

                foreach (var m in _registeredActionTypes)
                {
                    var a = m.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(ExternalIntegrationActionAttribute) && a.ConstructorArguments[0].Value.ToString() == action);

                    if (a == null)
                    {
                        continue;
                    }

                    return (IExternalIntegrationAction)Activator.CreateInstance(m);
                }

                return null;
            }
        }
    }
}
