using Services.Core.Composition;
using Services.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Core.Monitoring.Health
{
    public sealed class ComponentHealthMonitor
    {
        static ComponentHealthMonitor _defaultInstance;
        readonly IEnumerable<IDependentComponent> _registeredComponents;
        List<IDependentComponent> _components;
        static readonly object _mlock = new object();

        public static ComponentHealthMonitor Default
        {
            get
            {
                if (_defaultInstance == null)
                {
                    lock (_mlock)
                    {
                        _defaultInstance = new ComponentHealthMonitor();
                    };
                }

                return _defaultInstance;
            }
        }

        public void Dispose ()
        {
            _components.Clear();
        }

        private ComponentHealthMonitor ()
        {
            _components = new List<IDependentComponent>();
            _registeredComponents = Container.Default.GetAll<IDependentComponent>();
        }

        public IDependentComponent GetComponent (string componentIdentifier)
        {
            if (_components == null || !_components.Any() || !_components.Any(c => c.Id == componentIdentifier))
            {
                throw LocalErrors.ComponentNotFoundFound();
            }

            return _components.FirstOrDefault(c => c.Id == componentIdentifier);
        }

        public IEnumerable<IDependentComponent> GetComponents (DependentComponentTypes type)
        {
            return this[_components, type];
        }

        public ComponentHealthMonitor Configure<TConfiguration> (ICacheManager cache, params Tuple<DependentComponentTypes, TConfiguration>[] setting)
        {
            try
            {
                if (setting == null || !setting.Any())
                {
                    return this;
                }

                foreach (var s in setting)
                {
                    if (s == null || s.Item1 == DependentComponentTypes.None || s.Item2 == null)
                    {
                        continue;
                    }

                    var rc = this[_registeredComponents, s.Item1];

                    if (rc == null || rc == default(IEnumerable<IDependentComponent>))
                    {
                        continue;
                    }

                    ConfigureComponents(s, rc, cache);
                }
            }
            catch (Exception ex)
            {
                //do nothing
            }

            return this;
        }

        private void ConfigureComponents<TConfiguration> (Tuple<DependentComponentTypes, TConfiguration> s, IEnumerable<IDependentComponent> rc, ICacheManager cache)
        {
            foreach (var c in rc)
            {
                var component = c.CreatePart();
                component.Initialize(s.Item2, true, cache);
                _components.Add(component);
            }
        }

        public async Task<IEnumerable<DependentComponentHealth>> CheckHealth ()
        {
            try
            {
                List<IEnumerable<DependentComponentHealth>> r = new List<IEnumerable<DependentComponentHealth>>();

                foreach (var c in _components)
                {
                    var response = await c.CheckHealth();

                    if (response == null || !response.Any())
                    {
                        continue;
                    }

                    r.Add(response);
                }

                if (!r.Any())
                {
                    return null;
                }

                return r.SelectMany(x => x);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        IEnumerable<IDependentComponent> this[IEnumerable<IDependentComponent> collection, DependentComponentTypes type]
        {
            get
            {
                if (collection == null || !collection.Any() || type == DependentComponentTypes.None)
                {
                    return default(IEnumerable<IDependentComponent>);
                }

                var c = collection.Where(m =>
                {
                    var a = (ComponentTypeAttribute[])m.GetType().GetCustomAttributes(typeof(ComponentTypeAttribute), false);
                    if (a == null)
                    {
                        return false;
                    }

                    return a.Any(t => t.Type == type);
                });


                if (c == null || !c.Any())
                {
                    return default(IEnumerable<IDependentComponent>);
                }

                return c;
            }
        }
    }
}
