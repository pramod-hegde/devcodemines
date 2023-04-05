namespace Services.Integration.Sql
{
    public interface ISqlConfiguration
    {
        bool EnableRequestResponseLogging { get; set; }
        SqlExecutionModes ExecutionMode { get; set; }
        SqlCommandTypes CommandType { get; set; }
        string CommandText { get; set; }        
        ISqlAuthenticationConfiguration AuthenticationConfig { get; set; }
    }
}
