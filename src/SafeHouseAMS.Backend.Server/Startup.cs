using System;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SafeHouseAMS.Backend.Server.Services;
using SafeHouseAMS.BizLayer;
using SafeHouseAMS.DataLayer;
using SafeHouseAMS.Transport;
#if !DEBUG
using LettuceEncrypt;
using System.IO;
#endif

namespace SafeHouseAMS.Backend.Server
{
    /// <summary>
    /// Класс настройки сервера kestrel
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Конфигурация для настройки приложения
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="configuration"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Регистрация служб в DI
        /// </summary>
        /// <param name="services">коллекция служб DI</param>
        public void ConfigureServices(IServiceCollection services)
        {

#if !DEBUG
            var certPath = Configuration.GetValue<string>("CertPersist:Path");
            var certPass = Configuration.GetValue<string>("CertPersist:Password");
            if (!Directory.Exists(certPath))
                Directory.CreateDirectory(certPath);
            services
                .AddLettuceEncrypt()
                .PersistDataToDirectory(new DirectoryInfo(certPath), certPass);
#endif
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opts =>
                {
                    Configuration.Bind("oidc", opts);
                    opts.TokenValidationParameters.ValidateAudience = false;
                    //opts.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                });

            services.AddAuthorization();

            services
                .ConnectToDatabase(Configuration)
                .AddBizLogic(Configuration)
                .AddDtoMapping();

            services.AddGrpc();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] {"application/octet-stream"});
            });
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
                });
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            // must be added after UseRouting and before UseEndpoints
            app.UseGrpcWeb();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<SurvivorCatalogueService>().EnableGrpcWeb();
                endpoints.MapGrpcService<LifeSituationDocumentsCatalogueService>().EnableGrpcWeb();
                endpoints.MapGrpcService<EpisodesCatalogueService>().EnableGrpcWeb();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("bye bye");
                });
            });
        }
    }
}
