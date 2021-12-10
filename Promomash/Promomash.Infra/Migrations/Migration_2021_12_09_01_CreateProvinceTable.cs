using System.Data;

using FluentMigrator;

using Promomash.Common.Infra.Extensions;

namespace Promomash.Demo.Infra.Migrations
{
    [Tags("Development", "Testing", "Production")]
    [Migration(2021_12_09_01, "Creates provinces table")]
    public class Migration_2021_12_09_01_CreateProvinceTable : BasePromomashDemoMigration
    {
        private readonly string _countriesTableName = "Countries";
        private readonly string _provincesTableName = "Provinces";
        public override void Up()
        {
            Create.Table(_provincesTableName)
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("Title").AsString(255).NotNullable()
                .WithColumn("CountryId").AsInt64().NotNullable()
                .WithColumn("CreatedAt").AsDateTime2().NotNullable()
                .WithColumn("EditedAt").AsDateTime2().NotNullable()
                .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false);

            Create.ForeignKey(table: _provincesTableName, column: "CountryId", toTable: _countriesTableName, primaryColumn: "Id")
                .OnDelete(Rule.None);

            Create.Index(table: _provincesTableName, column: "Title");
            Create.Index(table: _provincesTableName, column: "CountryId");
        }

        public override void Down()
        {
            Delete.Index(table: _provincesTableName, column: "Title");
            Delete.Index(table: _provincesTableName, column: "CountryId");

            Delete.ForeignKey(table: _provincesTableName, column: "CountryId", toTable: _countriesTableName);

            Delete.Table(_provincesTableName);
        }
    }
}
