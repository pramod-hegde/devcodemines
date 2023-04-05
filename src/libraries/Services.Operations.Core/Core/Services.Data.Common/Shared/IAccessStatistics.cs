using System;

namespace Services.Data.Common
{
    public interface IAccessStatistics
    {
        void Start ();        
        void Stop ();      
        void AddProcessed ();
        void AddError (string dataItemId, Exception error);
        IAccessStatisticsSnapshot GetSnapshot ();
    }
}
