using Serilog;
using System.Diagnostics;

namespace BeerInventoryApi.Logger
{
    public static class LogService
    {
        private static ILogger _logger;
        public static ILogger Logger
        {
            //for testing purpose
            set
            {
                _logger = value;
            }
            get
            {
                return _logger;
            }
        }

        public static void InitLogger(ILogger logger)
        {
            _logger = logger;
        }

        public static void LogInfo(string className, string methodName, string message)
        {
            Logger.Information($"{className}.{methodName}:  {message}.");
        }

        public static void LogError(string className, string methodName, string message)
        {
            Logger.Error($"{className}.{methodName}:  {message}.");
        }

        public static void LogWarning(string className, string methodName, string message)
        {
            Logger.Warning($"{className}.{methodName}:  {message}.");
        }
    }
}
