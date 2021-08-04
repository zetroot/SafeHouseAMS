using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        public static async Task<int> Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateBootstrapLogger();
            try
            {
                Log.Information("Building web host");
                var host = CreateHostBuilder(args).Build();
                
                Log.Information("Applying migrations");
                using var scope = host.Services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<IDatabaseMigrator>();
                await db.MigrateAsync();
                
                Log.Information("Starting web host");
                await host.RunAsync();
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
                .UseSerilog((context, services, configuration) => configuration.ReadFrom.Configuration(context.Configuration))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}