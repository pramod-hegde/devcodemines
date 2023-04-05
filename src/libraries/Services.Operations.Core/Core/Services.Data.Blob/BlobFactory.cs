using Services.Core.Common;
using Services.Core.Composition;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Data.Blob
{
    class BlobFactory<TCache>
    {
        readonly IBlobAccessAdapterInstanceConfiguration<TCache> _configuration;

        IEnumerable<IBlobHandler> _handlers;

        internal BlobFactory(IBlobAccessAdapterInstanceConfiguration<TCache> configuration)
        {
            _configuration = configuration;
            LoadHandlers();
        }

        private void LoadHandlers()
        {
            if (_configuration.BlobHandlers != null && _configuration.BlobHandlers.Any()) 
            {
                _handlers = _configuration.BlobHandlers;
            }
            else
            {
                _handlers = Container.Default.GetAll<IBlobHandler>();
                Ensure.NotNullOrEmpty("ImmutableBlobHandlers", _handlers);
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

        IBlobHandler TargetHandler
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
