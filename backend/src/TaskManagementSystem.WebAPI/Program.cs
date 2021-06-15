using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using TaskManagementSystem.Web.Helpers;

namespace TaskManagementSystem.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                LoggerFactory.CreateStartupLoggerInstance();
                Log.Information("Starting TaskManagementSystem.WebAPI");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly!");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
