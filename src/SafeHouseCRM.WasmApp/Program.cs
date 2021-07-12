using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Radzen;
using SafeHouseCRM.BizLayer.Abstractions.Services;
using SafeHouseCRM.WasmApp.Services;

namespace SafeHouseCRM.WasmApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddAuthorizationCore();
            
            builder.Services.AddOidcAuthentication(options =>
            {
                // Replace the Okta placeholders with your Okta values in the appsettings.json file.
                options.ProviderOptions.Authority = builder.Configuration.GetValue<string>("Okta:Authority");
                options.ProviderOptions.ClientId = builder.Configuration.GetValue<string>("Okta:ClientId");

                options.ProviderOptions.ResponseType = "code";
            });

            builder.Services.AddSingleton<IPersonCatalogue, InMemoryPersonCatalogue>();
            
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<ContextMenuService>();
            
            await builder.Build().RunAsync();
        }
    }
}
