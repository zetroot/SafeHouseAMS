using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Radzen;
using SafeHouseAMS.BizLayer.ExploitationEpisodes;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.Survivors;
using SafeHouseAMS.BizLayer.Users;
using SafeHouseAMS.Transport;
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
                    configuration.Bind("oidc", options.ProviderOptions);
                });

            var backendUri = configuration.GetValue<string>("Backend");
            Console.WriteLine(backendUri);
            services.AddHttpClient("amsAPI", client => client.BaseAddress = new Uri(backendUri))
                .AddHttpMessageHandler(_ => new GrpcWebHandler(GrpcWebMode.GrpcWebText))
                .AddHttpMessageHandler(sp =>
                    sp.GetRequiredService<AuthorizationMessageHandler>()
                        .ConfigureHandler(new[] { backendUri }, new []{"appdata"}));

            services.AddScoped(sp =>
                {
                    // Create a gRPC-Web channel pointing to the backend server
                    var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("amsAPI");
                    var channel = GrpcChannel.ForAddress(backendUri, new GrpcChannelOptions { HttpClient = httpClient });
                    return channel;
                });

            services.AddDtoMapping();

            services.TryAddTransient<ISurvivorCatalogue, SurvivorCatalogueClient>();
            services.TryAddTransient<ILifeSituationDocumentsAggregate, LifeSituationDocumentsClient>();
            services.TryAddTransient<IEpisodesCatalogue, EpisodesCatalogueClient>();
            services.TryAddTransient<IUserCatalogue, UserCatalogueClient>();

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

        private static void ConfigureLogging(ILoggingBuilder builderLogging, [SuppressMessage("ReSharper", "UnusedParameter.Local")] IConfiguration builderConfiguration)
        {
            builderLogging.ClearProviders();
            Log.Logger = new LoggerConfiguration().WriteTo.BrowserConsole().MinimumLevel.Information().CreateLogger();
            builderLogging.AddSerilog();
        }
    }
}
