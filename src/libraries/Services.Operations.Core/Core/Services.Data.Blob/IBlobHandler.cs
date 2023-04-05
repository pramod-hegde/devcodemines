using Services.Core.Contracts;
using Services.Data.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Data.Blob
{
    public interface IBlobHandler : ICompositionPart
    {     
        void Initialize<TCache>(IBlobAccessAdapterInstanceConfiguration<TCache> setting);         
        void Dispose();         
        Task<IEnumerable<T>> ReadAsync<T>(object setting, CancellationToken cancellation);
        Task WriteAsync<TIn, TWriteSetting>(TIn dataItem, TWriteSetting writeSettings = default, CancellationToken cancellation = default);
    }

    public interface IBlobAccessAdapterInstanceConfiguration<TCache> : IDataAccessAdapterInstanceConfiguration<BlobConnection, TCache>
    {
        IEnumerable<IBlobHandler> BlobHandlers { get; set; }
    }
}
