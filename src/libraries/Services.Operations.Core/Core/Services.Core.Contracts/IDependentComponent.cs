using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Core.Contracts
{
    public interface IDependentComponent : ICompositionPart
    {
        IDependentComponent CreatePart ();
        void Initialize (dynamic setting, bool needsCaching = false, ICacheManager cacheManager = null);
        Task<IEnumerable<DependentComponentHealth>> CheckHealth ();
    }
}
