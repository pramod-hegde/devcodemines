namespace Services.Data.Common
{
    public sealed class DataSourceAdapterFactoryAdapter<TConfiguration> :
        DataAdapterFactoryAdapterBase<IDataAccessAdapterFactory<TConfiguration>, IDataAccessAdapter>,
        IDataAccessAdapterFactoryAdapter
    {
        public DataSourceAdapterFactoryAdapter (IDataAccessAdapterFactory<TConfiguration> factory, string displayName)
            : base(factory, displayName) { }
    }
}
