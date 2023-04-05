namespace Services.Core.Logging
{
    public sealed class LoggerFactory : ILoggerFactory
    {
        public LoggerFactory()
        {
            if (_builder == null)
            {
                _builder = new LoggerBuilder();
            }
        }

        readonly ILoggerBuilder _builder;
        ILoggerBuilder ILoggerFactory.DefaultBuilder => _builder;
    }
}