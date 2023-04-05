using Services.Core.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Core.Monitoring
{
    public abstract class AbstractMonitorBase<T, TWatchDog> : IDependentComponent
    {
        protected T _setting;
        protected TWatchDog _watchDog;
        protected List<DependentComponentHealth> _health;
        protected bool _needsCaching;
        protected ICacheManager _cacheManager;

        public AbstractMonitorBase ()
        {
            _health = new List<DependentComponentHealth>();
        }

        public abstract string Id { get; }

        public abstract Task<IEnumerable<DependentComponentHealth>> CheckHealth ();
        public abstract IDependentComponent CreatePart ();
        public abstract Task Initialize ();

        public virtual void Initialize (dynamic setting, bool needsCaching = false, ICacheManager cacheManager = null)
        {
            _setting = setting;
            _cacheManager = cacheManager;
            _needsCaching = needsCaching;
            Initialize();
        }
    }
}
