using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Integration.Wcf
{
    public interface IExternalWcfClient<TClient> : IExternalWcfClient
    {
        TClient Client { get; }
    }
}
