using System.Threading;
using System.Threading.Tasks;

namespace Services.Data.Common
{
    public interface IDataAccessAction
    {
        Task ExecuteAsync (IDataAccessAdapter source, IAccessStatistics statistics, CancellationToken cancellation);
    }
}
