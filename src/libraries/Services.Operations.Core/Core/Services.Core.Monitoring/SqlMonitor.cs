﻿using Services.Core.Contracts;
using Services.Core.Monitoring.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Warden;
using Warden.Watchers;
using Warden.Watchers.MsSql;

namespace Services.Core.Monitoring
{
    [Export(typeof(ICompositionPart))]
    [ComponentType(DependentComponentTypes.SqlServer)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class SqlMonitor : AbstractMonitorBase<ISqlDataStoreOperationContext, WardenWatchDog>
    {
        public override string Id => "MsSqlServerMonitor";

        SqlConnectionStringBuilder Connection => new SqlConnectionStringBuilder(_setting.SqlConnection)
        {
            Password = string.Empty
        };

        public override async Task<IEnumerable<DependentComponentHealth>> CheckHealth ()
        {
            if (_setting.Operation != DataOperationTypes.ConnectionCheck)
            {
                return null;
            }

            await _watchDog.ExecuteAsync();
            return _health;
        }

        public override IDependentComponent CreatePart ()
        {
            return new SqlMonitor();
        }

        public override Task Initialize ()
        {
            if (_setting == null || _setting.Operation != DataOperationTypes.ConnectionCheck)
            {
                return Task.CompletedTask;
            }

            ConfigureWatchDog();
            return Task.CompletedTask;
        }

        private void ConfigureWatchDog ()
        {
            if (_watchDog == null)
            {
                _watchDog = new WardenWatchDog();
            }

            _watchDog.AddWatcher<IWatcher>(MsSqlWatcher.Create(name: _setting.Id, connectionString: _setting.SqlConnection, configurator: config =>
            {
                config.WithQueryTimeout(_setting.OperationTimeout.HasValue ? _setting.OperationTimeout.Value : TimeSpan.FromSeconds(60));
                config.WithQuery("SELECT 1", null);                
            }));
            _watchDog.OnSuccess(c => OnCompletion(c));
            _watchDog.OnFailure(c => OnCompletion(c));
            _watchDog.OnError(e => HandleError(e));
        }

        private void HandleError (Exception e)
        {
            _health.Add(new DependentComponentHealth
            {
                ComponentType = DependentComponentTypes.SqlServer,
                IsValid = false,
                Connection = Connection?.ConnectionString,
                Identifier = _setting.Id,
                ErrorDetails = e.ToString(),
                Description = $"An error occurred in {Connection?.DataSource}"
            });
        }

        private void OnCompletion (IWardenCheckResult c)
        {
            _health.Add(new DependentComponentHealth
            {
                ComponentType = DependentComponentTypes.SqlServer,
                IsValid = c.IsValid,
                Connection = Connection?.ConnectionString,
                Identifier = _setting.Id,
                ErrorDetails = c.Exception?.ToString(),
                Description = c.WatcherCheckResult?.Description,
                ExecutionTime = c.ExecutionTime,
                StartedAt = c.StartedAt,
                CompletedAt = c.CompletedAt
            });
        }
    }
}
