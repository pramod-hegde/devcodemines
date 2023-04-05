using System.Threading;
using System.Threading.Tasks;

namespace Services.Data.Common
{
    public interface IDataAdapterFactoryAdapter<TDataAdapter> : IDataAdapterDefinition
    {
        Task<TDataAdapter> CreateAsync (object configuration, IDataAccessContext context, CancellationToken cancellation);
    }
}
