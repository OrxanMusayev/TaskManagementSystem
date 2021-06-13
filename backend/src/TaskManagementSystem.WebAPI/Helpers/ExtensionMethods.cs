using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace TaskManagementSystem.Web.Helpers
{
    public static class ExtensionMethods
    {
        public static void AddLogger(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        }
    }
}
