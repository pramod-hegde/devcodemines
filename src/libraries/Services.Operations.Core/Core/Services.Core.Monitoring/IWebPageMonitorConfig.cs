namespace Services.Core.Monitoring
{
    public interface IWebPageMonitorConfig
    {
        string Identifier { get; }
        string Url { get; set; }
    }
}
