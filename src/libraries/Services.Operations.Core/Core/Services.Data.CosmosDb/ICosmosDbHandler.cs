using Services.Core.Contracts;
using Services.Data.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Data.CosmosDb
{
    public interface ICosmosDbHandler : ICompositionPart
    {     
        void Initialize<TCache>(ICosmosDbAccessAdapterInstanceConfiguration<TCache> setting);         
        void Dispose();         
        Task<IEnumerable<T>> ReadAsync<T>(object setting, CancellationToken cancellation);
        Task WriteAsync<TIn, TWriteSetting>(TIn dataItem, TWriteSetting writeSettings = default(TWriteSetting), CancellationToken cancellation = default(CancellationToken));
    }

    public interface ICosmosDbAccessAdapterInstanceConfiguration<TCache> : IDataAccessAdapterInstanceConfiguration<CosmosDbConnection, TCache>
    {
        string Collection { get; set; }
        bool UseReadOnlyConnection { get; set; }
        IList<string> PreferredReadLocations { get; set; }
        IEnumerable<ICosmosDbHandler> Handlers { get; set; }
    }
}
