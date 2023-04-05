using Services.Data.Common;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Data.AzureServiceBus
{
    public sealed class AzureServiceBusAccessAdapter<TCache> : IDataAccessAdapter
    {
        object IDataAccessAdapter.AdapterSettings => throw new NotSupportedException();

        readonly DefaultMessageFactory<TCache> _messageFactory;

        public AzureServiceBusAccessAdapter (IAzureServiceBusAccessAdapterInstanceConfiguration<TCache> configuration)
        {
            _messageFactory = new DefaultMessageFactory<TCache>(configuration);
        }

        async Task IDataAccessAdapter.WriteAsync<TIn,TSetting> (TIn dataItem, TSetting setting, CancellationToken cancellation)
        {
            await _messageFactory.WriteAsync(dataItem, setting, cancellation);
        }
       
        async Task<IEnumerable<TOut>> IDataAccessAdapter.ReadAsync<TOut> (object setting, CancellationToken cancellation)
        {
           return await _messageFactory.ReadAsync<TOut>(setting, cancellation);
        }

        void IDisposable.Dispose ()
        {
            _messageFactory.Dispose();
        }
    }
}
