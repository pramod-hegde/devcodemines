using Services.Core.Common;
using Services.Core.Monitoring.Health;
using System;

namespace Services.Core.Monitoring.Health
{
    sealed class LocalErrors : CommonErrors
    {
        internal static Exception ComponentNotFoundFound ()
        {
            return new ArgumentException(FormatMessage(Resources.ComponentNotFoundFound));
        }
    }
}
