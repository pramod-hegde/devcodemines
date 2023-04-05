namespace Services.Integration.Sql
{
    public interface ISqlOperation
    {
        object Execute<TIn>(params TIn[] parameters);
    }
}
