using FluentMigrator;

namespace Promomash.Demo.Infra.Migrations
{
    /// <summary>
    /// Base migration class of PromomashDemo migrations
    /// </summary>
    public abstract class BasePromomashDemoMigration : Migration
    {
        /// <summary>
        /// Up migration
        /// </summary>
        public override abstract void Up();

        /// <summary>
        /// Down migration
        /// </summary>
        public override abstract void Down();
    }
}
