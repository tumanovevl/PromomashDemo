using System;
using System.IO;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using FluentMigrator.Runner;

using Serilog;
using Serilog.Core;

using Promomash.Demo.Common.Settings;
using Promomash.Demo.Infra.Context;

namespace Promomash
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost webHost = CreateWebHostBuilder(args).Build();

            LogAndRun(webHost);
        }

        /// <summary>
        /// Initialize WebHost
        /// </summary>        
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(ConfigConfiguration)
                .ConfigureLogging((_, logging) => logging.ClearProviders())
                .UseStartup<Startup>()
                .UseSerilog();

        private static void LogAndRun(IWebHost webHost)
        {
            Log.Logger = BuildLogger(webHost);

            try
            {
                ApplyMigrations(webHost);

                SeedInMemoryDemoData(webHost);

                Log.Information("Starting application");
                webHost.Run();
                Log.Information("Application stopped");
            }
            catch (Exception exception)
            {
                Log.Fatal(exception, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void ConfigConfiguration(WebHostBuilderContext builderContext, IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builderContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
        }

        private static Logger BuildLogger(IWebHost webhost)
        {
            return new LoggerConfiguration().ReadFrom
                .Configuration(webhost.Services.GetRequiredService<IConfiguration>())
                .CreateLogger();
        }

        /// <summary>
        /// Applies pending migrations
        /// </summary>
        private static void ApplyMigrations(IWebHost webHost)
        {
            try
            {
                using (var scope = webHost.Services.CreateScope())
                {
                    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                    if (runner.HasMigrationsToApplyUp())
                    {
                        Log.Information("Applying migrations...");
                        runner.MigrateUp();
                        Log.Information("Migrations applied successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Migrations application error");
                throw;
            }
        }

        private static void SeedInMemoryDemoData(IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<PromomashDemoContext>();
                
                DemoDataGenerator.Initialize(services);
            }
        }
    }
}
