using Services.Core.Common;
using Services.Core.Composition;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Data.CosmosDb
{
    class CosmosDbFactory<TCache>
    {
        readonly ICosmosDbAccessAdapterInstanceConfiguration<TCache> _configuration;

        IEnumerable<ICosmosDbHandler> _handlers;

        internal CosmosDbFactory(ICosmosDbAccessAdapterInstanceConfiguration<TCache> configuration)
        {
            _configuration = configuration;
            LoadHandlers();
        }

        private void LoadHandlers()
        {
            if (_configuration.Handlers != null && _configuration.Handlers.Any())
            {
                _handlers = _configuration.Handlers;
            }
            else
            {
                _handlers = Container.Default.GetAll<ICosmosDbHandler>();
                Ensure.NotNullOrEmpty("ImmutableCosmosDbHandlers", _handlers);
            }

            InitializeHandlers();
        }

        private void InitializeHandlers()
        {
            foreach (var h in _handlers)
            {
                h.Initialize(_configuration);
            }
        }
       

        internal void Dispose()
        {
            foreach (var h in _handlers)
            {
                h.Dispose();
            }

            _handlers = null;
        }

        ICosmosDbHandler TargetHandler
        {
            get
            {
                return _handlers.First();
            }
        }   
   
        internal async Task WriteAsync<TIn, TSetting>(TIn dataItem, TSetting setting, CancellationToken cancellation)
        {
            await TargetHandler.WriteAsync(dataItem, setting, cancellation);
        }

        internal async Task<IEnumerable<TOut>> ReadAsync<TOut>(object setting, CancellationToken cancellation)
        {
            return await TargetHandler.ReadAsync<TOut>(setting, cancellation);
        }

    }
}
