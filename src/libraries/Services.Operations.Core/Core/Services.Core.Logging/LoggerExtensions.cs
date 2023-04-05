using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections.Generic;

namespace Services.Core.Logging.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogTrace(this ILogger logger, string message, IDictionary<string, object> additionalProperties = null, string sequence = "", SeverityLevel? severityLevel = null, DateTimeOffset? timeStamp = null)
        {
            logger.Log(LogLevel.Trace, new TraceTelemetryLog
            {
                Message = message,
                Sequence = sequence,
                SeverityLevel = severityLevel,
                Timestamp = timeStamp ?? DateTimeOffset.UtcNow
            }, additionalProperties);
        }

        public static void LogEvent(this ILogger logger, string message, IDictionary<string, object> additionalProperties = null, string sequence = "", SeverityLevel? severityLevel = null, DateTimeOffset? timeStamp = null)
        {
            logger.Log(LogLevel.Event, new EventTelemetryLog
            {
                Name = message,
                Sequence = sequence,
                Timestamp = timeStamp ?? DateTimeOffset.UtcNow,
            }, additionalProperties);
        }

        public static void LogDependency(this ILogger logger, string dependencyName, TimeSpan duration, bool result, string responseCode = "", string dependencyType = "", string id = "", string command = "", IDictionary<string, object> additionalProperties = null)
        {
            logger.Log(LogLevel.Dependency, new DependencyTelemetryLog
            {
                Data = command,
                Name = dependencyName,
                Duration = duration,
                Id = id,
                ResultCode = responseCode,
                Success = result,
                Type = dependencyType
            }, additionalProperties);
        }

        public static void LogException(this ILogger logger, Exception exception, string message = "", IDictionary<string, object> additionalProperties = null, string problemId = "", string sequence = "", SeverityLevel? severityLevel = null, DateTimeOffset? timeStamp = null)
        {
            logger.Log(LogLevel.Exception, new ExceptionTelemetryLog
            {
                Message = message,
                Exception = exception,
                Sequence = sequence,
                Timestamp = timeStamp ?? DateTimeOffset.UtcNow,
                SeverityLevel = severityLevel,
                ProblemId = problemId

            }, additionalProperties);
        }
    }
}