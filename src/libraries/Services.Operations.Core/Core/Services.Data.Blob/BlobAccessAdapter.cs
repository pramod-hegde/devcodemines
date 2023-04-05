using Services.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Data.Blob
{
    public class BlobAccessAdapter<TCache> : IDataAccessAdapter
    {
        private IBlobAccessAdapterInstanceConfiguration<TCache> cosmosDbAccessAdapterInstanceConfiguration;

        readonly BlobFactory<TCache> _factory;

        public object AdapterSettings => throw new NotImplementedException();

        public BlobAccessAdapter(IBlobAccessAdapterInstanceConfiguration<TCache> configuration)
        {
            _factory = new BlobFactory<TCache>(configuration);
        }

        async Task IDataAccessAdapter.WriteAsync<TIn, TSetting>(TIn dataItem, TSetting setting, CancellationToken cancellation)
        {
            await _factory.WriteAsync(dataItem, setting, cancellation);
        }

        async Task<IEnumerable<TOut>> IDataAccessAdapter.ReadAsync<TOut>(object setting, CancellationToken cancellation)
        {
            return await _factory.ReadAsync<TOut>(setting, cancellation);
        }

        void IDisposable.Dispose()
        {
            _factory.Dispose();
        }
    }
}
