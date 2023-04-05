using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Services.Integration.Core
{
    public interface IServiceModule<TModuleConfiguration>
    {
        Task<TOut> Execute<TIn, TOut>(TIn input);
    }
}
