using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Promomash.Demo.Common.Interfaces;
using Promomash.Demo.Common.Settings;
using Promomash.Demo.Infra.Context;

namespace Promomash.Demo.Infra.Configuration
{
    /// <summary>
    /// PromomashDemo's infrastructure layer DI extensions
    /// </summary>
    public static class PromomashDemoInfraLayerExtensions
    {
        /// <summary>
        /// Add a PromomashDemo unit of work resolving
        /// </summary>
        /// <param name="services">Service collection instance</param>
        public static void AddPromomashDemoUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        /// <summary>
        /// Add a PromomashDemo context resolving
        /// </summary>
        /// <param name="services">Service collection instance</param>
        /// <param name="isDevelopment">Is current environment is development</param>
        public static void AddPromomashDemoContext(this IServiceCollection services, bool isDevelopment)
        {
            services.AddDbContext<PromomashDemoContext>(opt => opt.UseInMemoryDatabase("PromomashDemoDev"));

            //services.AddDbContext<PromomashDemoContext>((IServiceProvider serviceProvider, DbContextOptionsBuilder optionsBuilder) =>
            //{
            //    var settings = serviceProvider.GetService<IOptionsMonitor<PromomashDemoSettings>>();
            //    var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            //    optionsBuilder
            //        .UseSqlServer(settings.CurrentValue.ConnectionString)
            //        .UseLoggerFactory(loggerFactory);

            //    if (isDevelopment)
            //    {
            //        optionsBuilder.EnableSensitiveDataLogging(true);
            //    }
            //});
        }
    }
}
