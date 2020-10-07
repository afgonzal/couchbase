using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;

namespace CouchPOC
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Connecting to Couchbase");
            using (ServiceProvider svcProvider = ConfigureDependencyInjection(LoadConfiguration()))
            {
                var app = svcProvider.GetService<TestCouch>();
                await app.Run();
            }
            Console.WriteLine("Finished");
        }

        private static ServiceProvider ConfigureDependencyInjection(IConfiguration config)
        {
            var services = new ServiceCollection();
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                loggingBuilder.AddNLog(config);
            });
            services.AddScoped<IConfiguration>(provider => config);

            CouchPOC.Data.Startup.ConfigureServices(services, config);
            services.AddTransient<TestCouch>();
            return services.BuildServiceProvider();
        }

        public static ILoggerFactory LoggingConfiguration()
        {
            ILoggerFactory loggerFactory = new LoggerFactory();

            loggerFactory.AddNLog(new NLogProviderOptions
            {
                CaptureMessageTemplates = true,
                CaptureMessageProperties = true
            });

            LogManager.LoadConfiguration("nlog.config");
            return loggerFactory;
        }

        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true,
                    reloadOnChange: true);
            //.AddUserSecrets<Program>();

            return builder.Build();
        }
    }
}
