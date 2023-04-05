using System;

namespace Services.Core.Contracts
{
    [Serializable]
    public sealed class DependentComponentHealth
    {
        public bool IsValid { get; set; }
        public DependentComponentTypes ComponentType { get; set; }
        public string Identifier { get; set; }        
        public string Connection { get; set; }        
        public string ErrorDetails { get; set; }
        public string Description { get; set; }
        public TimeSpan ExecutionTime { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
