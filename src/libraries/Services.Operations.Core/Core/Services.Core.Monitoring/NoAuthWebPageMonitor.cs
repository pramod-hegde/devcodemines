using Services.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Warden;
using Warden.Watchers;

namespace Services.Core.Monitoring
{
    [Export(typeof(ICompositionPart))]
    [ComponentType(DependentComponentTypes.NoAuthEndpoint)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class NoAuthWebPageMonitor : AbstractMonitorBase<IWebPageMonitorConfig, WardenWatchDog>
    {
        public override string Id => "NoAuthWebPageMonitor";

        public override async Task<IEnumerable<DependentComponentHealth>> CheckHealth ()
        {
            await _watchDog.ExecuteAsync();
            return _health;
        }

        public override IDependentComponent CreatePart ()
        {
            return new NoAuthWebPageMonitor();
        }

        public override Task Initialize ()
        {
            if (_setting == null)
            {
                return Task.CompletedTask;
            }

            _watchDog.AddWatcher<IWatcher>(Warden.Watchers.Web.WebWatcher.Create(url:_setting.Url));
            _watchDog.OnSuccess(c => OnCompletion(c));
            _watchDog.OnFailure(c => OnCompletion(c));
            _watchDog.OnError(e => HandleError(e));

            return Task.CompletedTask;
        }

        private void HandleError (Exception e)
        {
            _health.Add(new DependentComponentHealth
            {
                ComponentType = DependentComponentTypes.SqlServer,
                IsValid = false,
                Connection = _setting.Url,
                Identifier = _setting.Identifier,
                ErrorDetails = e.ToString()
            });
        }

        private void OnCompletion (IWardenCheckResult c)
        {
            _health.Add(new DependentComponentHealth
            {
                ComponentType = DependentComponentTypes.SqlServer,
                IsValid = c.IsValid,
                Connection = _setting.Url,
                Identifier = _setting.Identifier,
                ErrorDetails = c.Exception?.ToString(),
                Description = c.WatcherCheckResult?.Description,
                ExecutionTime = c.ExecutionTime,
                StartedAt = c.StartedAt,
                CompletedAt = c.CompletedAt
            });
        }
    }
}
