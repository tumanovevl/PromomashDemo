namespace Promomash.Demo.Common.Settings
{
    /// <summary>
    /// Promomash settings
    /// </summary>
    public class PromomashDemoSettings
    {
        /// <summary>
        /// DB context connection string
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Disable ChangeTracker.AutoDetectChangesEnabled 
        /// </summary>
        public bool DisableAutoDetectChanges { get; set; }
    }
}
