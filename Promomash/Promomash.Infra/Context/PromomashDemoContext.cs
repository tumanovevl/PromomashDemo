using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Promomash.Common.Infra.Context;
using Promomash.Common.Interfaces;
using Promomash.Demo.Common.Settings;

namespace Promomash.Demo.Infra.Context
{
    /// <summary>
    /// Database context for PromomashDemo app
    /// </summary>
    public class PromomashDemoContext : EFContext
    {
        public PromomashDemoContext(
            DbContextOptions<PromomashDemoContext> options,
            IDateTime dateTime,
            bool autoDetectChangesEnabled
            )
            : base(options, dateTime)
        {
            ChangeTracker.AutoDetectChangesEnabled = autoDetectChangesEnabled;
        }

        public PromomashDemoContext(
            DbContextOptions<PromomashDemoContext> options,
            ILogger<PromomashDemoContext> logger,
            IOptionsMonitor<PromomashDemoSettings> contextSettings,
            IDateTime dateTime
            )
            : base(options, logger, dateTime)
        {
            ChangeTracker.AutoDetectChangesEnabled = !contextSettings.CurrentValue.DisableAutoDetectChanges;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplyMappingConfigurationFromAssembly(modelBuilder, Assembly.GetExecutingAssembly());
        }
    }
}
