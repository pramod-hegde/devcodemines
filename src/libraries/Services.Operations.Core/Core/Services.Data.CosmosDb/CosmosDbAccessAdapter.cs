using Services.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Data.CosmosDb
{
    public class CosmosDbAccessAdapter<TCache> : IDataAccessAdapter
    {
        private ICosmosDbAccessAdapterInstanceConfiguration<TCache> cosmosDbAccessAdapterInstanceConfiguration;

        readonly CosmosDbFactory<TCache> _cosmosDbFactory;

        public object AdapterSettings => throw new NotImplementedException();

        public CosmosDbAccessAdapter(ICosmosDbAccessAdapterInstanceConfiguration<TCache> configuration)
        {
            _cosmosDbFactory = new CosmosDbFactory<TCache>(configuration);
        }

        async Task IDataAccessAdapter.WriteAsync<TIn, TSetting>(TIn dataItem, TSetting setting, CancellationToken cancellation)
        {
            await _cosmosDbFactory.WriteAsync(dataItem, setting, cancellation);
        }

        async Task<IEnumerable<TOut>> IDataAccessAdapter.ReadAsync<TOut>(object setting, CancellationToken cancellation)
        {
            return await _cosmosDbFactory.ReadAsync<TOut>(setting, cancellation);
        }

        void IDisposable.Dispose()
        {
            _cosmosDbFactory.Dispose();
        }
    }
}
