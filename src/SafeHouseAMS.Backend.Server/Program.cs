using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SafeHouseAMS.BizLayer;
using SafeHouseAMS.DataLayer;
using Serilog;
using Serilog.Events;

namespace SafeHouseAMS.Backend.Server
{
    /// <summary>
    /// Базовый клас приложения
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Program
    {
        /// <summary>
        /// точка входа в приложение
        /// </summary>
        /// <param name="args">Аргументы запуска</param>
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .ConfigureServices((builder, services) =>
                        {
                            services
                                .ConnectToDatabase(builder.Configuration)
                                .AddBizLogic(builder.Configuration);
                        })
                        .UseStartup<Startup>();
                });
    }
}