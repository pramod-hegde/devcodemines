namespace Services.Core.Contracts
{
    public enum EventSource
    {
        Default = 0,
        ApplicationInsights = 1,
        EventLog = 2,
        Sql = 4
    }
}
