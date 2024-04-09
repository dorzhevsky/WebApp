using NLog;

namespace Shared.LoggerManager
{
    public class LoggingManager : ILoggingManager
    {
        private static string LoggerName = nameof(LoggingManager);
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public void LogDebug(string message) => logger.Log(GetType(), new LogEventInfo(LogLevel.Debug, LoggerName, message));
        public void LogError(string message) => logger.Log(GetType(), new LogEventInfo(LogLevel.Error, LoggerName, message));
        public void LogInfo(string message) => logger.Log(GetType(), new LogEventInfo(LogLevel.Info, LoggerName, message));
        public void LogWarn(string message) => logger.Log(GetType(), new LogEventInfo(LogLevel.Warn, LoggerName, message));
    }
}
