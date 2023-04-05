using System;

namespace Services.Data.Common
{
    public interface IDataAdapterDefinition
    {
        string DisplayName { get; }
        string Description { get; }
        Type ConfigurationType { get; }
    }
}
