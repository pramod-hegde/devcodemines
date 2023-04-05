using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.Core.Monitoring
{
    public abstract class WatchDogBase<TBuilder, T1, T2>
    {
        protected TBuilder _builder;
        internal abstract void AddWatcher<TWatcher> (TWatcher watcher);
        internal abstract void OnError (params Expression<Action<Exception>>[] handlers);
        internal abstract void OnStart (params Expression<Action<T1>>[] handlers);
        internal abstract void OnSuccess (params Expression<Action<T2>>[] handlers);
        internal abstract void OnFailure (params Expression<Action<T2>>[] handlers);
        internal abstract Task ExecuteAsync ();
    }
}
