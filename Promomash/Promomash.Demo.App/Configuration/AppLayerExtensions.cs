using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

using MediatR;

using Promomash.Demo.App.Common.Behaviours;
using Promomash.Demo.App.Middlewares;

namespace Promomash.Demo.App.Configuration
{
    /// <summary>
    /// PromomashDemo's app layer DI extensions
    /// </summary>
    public static class AppLayerExtensions
    {
        /// <summary>
        /// Register custom exception handler middleware
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }

        /// <summary>
        /// Add all application layer services
        /// </summary>
        /// <param name="services">Service collection</param>
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        }
    }
}
