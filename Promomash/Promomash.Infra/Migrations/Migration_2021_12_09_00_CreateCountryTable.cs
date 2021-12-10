using FluentMigrator;

using Promomash.Common.Infra.Extensions;

namespace Promomash.Demo.Infra.Migrations
{
    [Tags("Development", "Testing", "Production")]
    [Migration(2021_12_09_00, "Creates countries table")]
    public class Migration_2021_12_09_00_CreateCountryTable : BasePromomashDemoMigration
    {
        private readonly string _countriesTableName = "Countries";
        public override void Up()
        {
            Create.Table(_countriesTableName)
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("Title").AsString(255).NotNullable()
                .WithColumn("CreatedAt").AsDateTime2().NotNullable()
                .WithColumn("EditedAt").AsDateTime2().NotNullable()
                .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false);

            Create.Index(table: _countriesTableName, column: "Title").Unique();
        }
        public override void Down()
        {
            Delete.Index(table: _countriesTableName, column: "Title");

            Delete.Table(_countriesTableName);
        }
    }
}
