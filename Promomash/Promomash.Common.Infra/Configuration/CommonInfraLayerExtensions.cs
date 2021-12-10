using Microsoft.Extensions.DependencyInjection;

using Promomash.Common.Infra.Common;
using Promomash.Common.Interfaces;

namespace Promomash.Common.Infra.Configuration
{
    /// <summary>
    /// Common's infra layer DI extensions
    /// </summary>
    public static class CommonInfraLayerExtensions
    {
        public static void AddDateTimeConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IDateTime, MachineDateTime>();
        }
    }
}
