using Services.Core.Common;
using Services.Core.Composition;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Data.AzureServiceBus
{
    public class DefaultMessageFactory<TCache>
    {
        readonly IAzureServiceBusAccessAdapterInstanceConfiguration<TCache> _configuration;

        IEnumerable<IMessagingHandler> _handlers;

        internal DefaultMessageFactory (IAzureServiceBusAccessAdapterInstanceConfiguration<TCache> configuration)
        {
            _configuration = configuration;
            LoadHandlers();           
        }

        private void LoadHandlers ()
        {
            if (_configuration.MessageHandlers != null && _configuration.MessageHandlers.Any())
            {
                _handlers = _configuration.MessageHandlers;
            }
            else
            {
                _handlers = Container.Default.GetAll<IMessagingHandler>();

                Ensure.NotNullOrEmpty("ImmutableMessageHandlers", _handlers);               
            }

            InitializeHandlers();
        }

        private void InitializeHandlers ()
        {
            foreach (var h in _handlers)
            {                          
                h.Initialize(_configuration);
            }
        }

        internal async Task WriteAsync<TIn,TSetting> (TIn dataItem, TSetting setting, CancellationToken cancellation)
        {
            await TargetHandler.WriteAsync(dataItem, setting, cancellation);
        }

        internal async Task<IEnumerable<TOut>> ReadAsync<TOut> (object setting, CancellationToken cancellation)
        {
            return await TargetHandler.ReadAsync<TOut>(setting, cancellation);
        }

        internal void Dispose ()
        {
            foreach (var h in _handlers)
            {
                h.Dispose();
            }

            _handlers = null;
        }

        IMessagingHandler TargetHandler
        {
            get
            {
                return _handlers.First(h => h.Type == _configuration.TargetType);
            }
        }
    }
}
