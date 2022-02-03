using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Snt.Romashka.WebApi;

namespace Snt.Romashka.Host
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        private ILifetimeScope AutofacContainer { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var webApiAssemblyPath = Directory.GetFiles(@".\").FirstOrDefault(_ => _.EndsWith("WebApi.dll"));
            var assemblyName = AssemblyLoadContext.GetAssemblyName(webApiAssemblyPath ?? 
                throw new InvalidOperationException("Assembly '..WebApi' was not found"));
            var webApiAssembly = Assembly.Load(assemblyName);
            services.AddOptions();
            
            // TODO: Временно отключено
            // services.AddTransient<IAuthenticationHandler, TokenAuthenticationHandler>();
            // services.AddTransient<IAuthorizationHandler, CustomAuthorizationHandler>();
            // services.AddTransient<IAuthorizationPolicyProvider, CustomAuthPolicyProvider>();
            // services.AddAuthentication("Token")
            //     .AddScheme<TokenAuthenticationOptions, TokenAuthenticationHandler>("Token", null);
            // services.AddAuthorization(options =>
            // {
            //     options.DefaultPolicy = new AuthorizationPolicyBuilder()
            //         .RequireAuthenticatedUser()
            //         .AddAuthenticationSchemes(TokenAuthenticationOptions.SchemeName)
            //         .Build();
            // });

            services.AddControllers()
                .AddApplicationPart(webApiAssembly)
                .AddControllersAsServices()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                });

            services.AddCors();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger<Startup>();
            app.Use(async (context, next) => await ErrorHandle(context, next, logger));
            app.UseCors(builder =>
            {
                var origins = _configuration.GetSection("HttpServer:Endpoints").GetChildren().Select(_ =>
                {
                    var endpoint = new EndpointConfiguration();
                    _.Bind(endpoint);
                    return endpoint.CorsOrigins;
                }).ToArray();
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithOrigins(origins)
                    .SetIsOriginAllowed(h => true);
            });
            if (env.IsDevelopment() || env.EnvironmentName == "dev-hub")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();
            app.UseStaticFiles();
            // app.UseAuthentication();
            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }

        private async Task ErrorHandle(HttpContext context, Func<Task> next, ILogger<Startup> logger)
        {
            try
            {
                await next.Invoke();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                logger.LogError(e, e.Message);
                await next.Invoke();
            }
        }
    }
}