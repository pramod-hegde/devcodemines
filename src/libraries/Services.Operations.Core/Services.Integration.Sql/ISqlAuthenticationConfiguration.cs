namespace Services.Integration.Sql
{
    public delegate object SqlConnectionAuthCallback();
    public interface ISqlAuthenticationConfiguration
    {
        object Configuration { get; set; }
        SqlConnectionAuthCallback AuthenticationCallback { get; }
    }
}
