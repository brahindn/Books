
using Contracts;
using NLog;

namespace LoggerService
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public LoggerManager() 
        {
        }

        public void LogDebug(string message) => logger.Debug(message);

        public void LogError(string massage) => logger.Error(massage);

        public void LogInfo(string massage) => logger.Info(massage);

        public void LogWarning(string massage) => logger.Warn(massage);
    }
}
