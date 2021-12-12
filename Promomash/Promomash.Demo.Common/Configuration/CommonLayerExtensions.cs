using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Promomash.Demo.Common.Settings;

namespace Promomash.Demo.Common.Configuration
{
    /// <summary>
    /// Domain's infra layer DI extensions
    /// </summary>
    public static class CommonLayerExtensions
    {
        /// <summary>
        /// Binding a PromomashSettings class to coressponding section in configuration files
        /// </summary>
        /// <param name="services">Service collection instance</param>
        /// <param name="configuration">Configaration instance</param>
        public static void AddPromomashDemoSettingsConfigurationSection(
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
            services.Configure<PromomashDemoSettings>(configuration.GetSection(nameof(PromomashDemoSettings)));
        }
    }
}
