using System.Threading;
using System.Threading.Tasks;

namespace Services.Data.Common
{
    public interface IDataAccessAdapterFactory<in TConfiguration> : IDataAdapterFactory
    {
        Task<IDataAccessAdapter> CreateAsync (TConfiguration configuration, IDataAccessContext context, CancellationToken cancellation);
    }
}
