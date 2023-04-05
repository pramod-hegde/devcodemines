using System;

namespace Services.Core.FaultHandling.Shared
{
    public partial class Context
    {
        internal static readonly Context None = new Context();

        private Guid? _correlationId;
        
        public Context (string operationKey)
        {
            OperationKey = operationKey;
        }

        public Context ()
        {
        }
      
        public string OperationKey { get; }
       
        public Guid CorrelationId
        {
            get
            {
                if (!_correlationId.HasValue) { _correlationId = Guid.NewGuid(); }
                return _correlationId.Value;
            }
        }

    }
}