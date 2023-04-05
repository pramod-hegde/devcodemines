using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Data.Common
{
    public interface IDataAccessAdapter : IDisposable
    {
        object AdapterSettings { get; }

        Task WriteAsync<TIn, TSetting> (TIn dataItem, TSetting setting, CancellationToken cancellation);

        Task<IEnumerable<TOut>> ReadAsync<TOut> (object setting, CancellationToken cancellation);
    }
}
