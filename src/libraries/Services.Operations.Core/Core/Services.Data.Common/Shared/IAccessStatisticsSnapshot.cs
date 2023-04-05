using System;
using System.Collections.Generic;

namespace Services.Data.Common
{
    public interface IAccessStatisticsSnapshot
    {
        TimeSpan ElapsedTime { get; }
        int Processed { get; }
        int Failed { get; }
        IReadOnlyCollection<KeyValuePair<string, string>> GetErrors ();
    }
}
