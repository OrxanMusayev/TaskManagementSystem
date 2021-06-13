using Serilog;
using System;
using System.IO;

namespace TaskManagementSystem.Web.Helpers
{
    public class LoggerFactory
    {
        /// <summary>
        /// Create logger to write startup logs, to better understanding application running
        /// </summary>
        public static void CreateStartupLoggerInstance()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.File(path: Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs/startupLogs.txt"),
                    rollingInterval: RollingInterval.Month)
                .MinimumLevel.Debug()
                .CreateLogger();
        }
    }
}
