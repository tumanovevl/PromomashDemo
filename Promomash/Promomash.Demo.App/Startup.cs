using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Promomash.Common.Infra.Configuration;
using Promomash.Demo.App.Configuration;
using Promomash.Demo.Common.Configuration;
using Promomash.Demo.Common.Settings;
using Promomash.Demo.Infra.Configuration;
using Promomash.Demo.Infra.Migrations;

namespace Promomash
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        /// <summary>
        /// Startup constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="environment"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this._configuration = configuration;
            this._environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDateTimeConfiguration();

            services.AddApplication();

            services.AddCors();
            services.AddOptions();
            services.AddControllersWithViews()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(options => options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)
                .AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true)
                .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            
            services.AddPromomashDemoSettingsConfigurationSection(this._configuration);

            services.AddSingleton(this._configuration);

            services.AddPromomashDemoUnitOfWork();

            services.AddPromomashDemoContext(isDevelopment: this._environment.IsDevelopment());

            services.AddSwaggerDocument(configure =>
            {
                configure.Title = "PromomashDemo API";
            });

            AddFluentMigrator(services);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        /// <summary>
        /// Register FluentMigrator
        /// </summary>
        private void AddFluentMigrator(IServiceCollection services)
        {
            services.AddFluentMigratorCore()
                            .ConfigureRunner(builder => builder
                                .AddSqlServer()
                                .WithGlobalConnectionString(provider =>
                                    provider.GetService<IOptionsMonitor<PromomashDemoSettings>>().CurrentValue.ConnectionString)
                                .ScanIn(typeof(BasePromomashDemoMigration).Assembly).For.Migrations());

            services.Configure<RunnerOptions>(runnerOptions =>
            {
                if (!string.IsNullOrEmpty(this._environment.EnvironmentName))
                {
                    runnerOptions.Tags = new[] { this._environment.EnvironmentName };
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseCustomExceptionHandler();

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            if (!this._environment.IsProduction())
            {
                app.UseSwaggerUi3(settings => {
                    settings.Path = "/api";
                    settings.DocumentPath = "/api/specification.json";
                });
            }

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                    //spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}
