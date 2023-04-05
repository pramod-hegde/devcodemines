using System;
using System.Globalization;

namespace Services.Data.Common
{
    public abstract class DynamicResourcesBase
    {
        protected static string Format (string format, params object[] args)
        {
            return String.Format(CultureInfo.InvariantCulture, format, args);
        }
    }
}
