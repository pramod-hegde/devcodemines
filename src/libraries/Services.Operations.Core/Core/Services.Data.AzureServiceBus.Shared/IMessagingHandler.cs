using Services.Core.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Data.AzureServiceBus
{
    public interface IMessagingHandler : ICompositionPart
    {
        void Initialize<TCache>(IAzureServiceBusAccessAdapterInstanceConfiguration<TCache> setting);
        ImmutableMessageTargetTypes Type { get; }
        Task<IEnumerable<T>> ReadAsync<T> (object setting, CancellationToken cancellation);
        Task WriteAsync<TIn, TWriteSetting> (TIn dataItem, TWriteSetting writeSettings = default(TWriteSetting), CancellationToken cancellation = default(CancellationToken));
        void Dispose ();
    }
}
