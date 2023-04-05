using Services.Data.Common.Shared;
using System;

namespace Services.Data.Common
{
    public sealed class DynamicConfigurationResources : DynamicResourcesBase
    {
        public static string LocationMode
        {
            get
            {
                return Format(ConfigurationResources.LocationModeFormat, Defaults.Current.LocationMode,
                    String.Join(", ", Enum.GetNames(typeof(AzureStorageLocationMode))));
            }
        }
      
        private DynamicConfigurationResources () { }
    }
}
