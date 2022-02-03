using System;
using System.IO;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Configuration;
using NLog;
using NLog.Web;

namespace Snt.Romashka.Host
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var configureNLog = NLogBuilder.ConfigureNLog("nlog.config");
            var logger = configureNLog.GetCurrentClassLogger();
            var nlogOptions = new NLogAspNetCoreOptions()
            {
                IncludeScopes = true,
            };
            try
            {
                logger.Debug("init main");
                var config = StartupHelper.GetConfiguration(args);
                var host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                    .ConfigureLogging((loggingBuilder) =>
                    {
                        loggingBuilder.AddNLog(configureNLog.Configuration);
                    })
                    .UseNLog(nlogOptions)
                    .ConfigureWebHostDefaults(builder =>
                    {
                        builder.UseConfiguration(config)
                            .UseKestrel(options => options.ConfigureEndpoints())
                            .UseContentRoot(Directory.GetCurrentDirectory())
                            .UseUrls("http://localhost:5051")
                            .UseWebRoot("wwwroot")
                            .UseStartup<Startup>();
                    })
                    .Build();

                host.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                logger.Error(e, "Stopped program because of exception");
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }

        }
    }
}