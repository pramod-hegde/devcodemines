using System.Collections.Generic;
using System.Linq;

namespace Services.Core.Logging
{
    class ConcreateLoggerProvider : IConcreateLoggerProvider
    {
        private LoggerBuilder _loggers;

        public ConcreateLoggerProvider(LoggerBuilder loggers)
        {
            _loggers = loggers;
        }

        List<ILogger> _items = new List<ILogger>();
        ILogger[] IConcreateLoggerProvider.Loggers
        {
            get
            {
                if (!_items.Any())
                {
                    for (int i = 0; i < _loggers.Count; i++)
                    {
                        _items.Add(_loggers[i]);
                    }
                }

                return _items.ToArray();
            }
        }
    }
}