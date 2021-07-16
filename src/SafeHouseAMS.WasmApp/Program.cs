using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Radzen;
using SafeHouseAMS.BizLayer;
using SafeHouseAMS.BizLayer.Survivors;
using SafeHouseAMS.WasmApp.Services;
using Serilog;

namespace SafeHouseAMS.WasmApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            ConfigureLogging(builder.Logging, builder.Configuration);
            ConfigureServices(builder.Services, builder.Configuration);

            builder.Services.TryAddSingleton<ISurvivorRepository, InMemorySurvivorsRepository>();
            
            await builder.Build().RunAsync();
        }
        
        /// <summary>
        /// Конфигурация DI-контейнера
        /// </summary>
        /// <param name="services">Коллекция служб - собственно контейнер</param>
        /// <param name="configuration">Конфигурация</param>
        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthorizationCore()
                .AddOidcAuthentication(options =>
                {
                    // Replace the Okta placeholders with your Okta values in the appsettings.json file.
                    options.ProviderOptions.Authority = configuration.GetValue<string>("Okta:Authority");
                    options.ProviderOptions.ClientId = configuration.GetValue<string>("Okta:ClientId");

                    options.ProviderOptions.ResponseType = "code";
                });
            
            services.AddBizLogic(configuration);
                
            services.AddScoped<DialogService>()
                .AddScoped<NotificationService>()
                .AddScoped<TooltipService>()
                .AddScoped<ContextMenuService>();
        }
        
        /// <summary>
        /// Настройка логирования.
        /// По умолчанию используется Serilog. Конфигурация логгера задаётся через IConfiguration, из секции "Serilog"
        /// </summary>
        /// <param name="builderLogging">Билдер логгера</param>
        /// <param name="builderConfiguration">Конфигурация</param>
        private static void ConfigureLogging(ILoggingBuilder builderLogging, IConfiguration builderConfiguration)
        {
            builderLogging.ClearProviders();
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builderConfiguration, "Serilog").CreateLogger();
            builderLogging.AddSerilog();
        }
    }
}
