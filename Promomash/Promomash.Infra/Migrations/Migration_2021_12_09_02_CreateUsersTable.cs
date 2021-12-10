using System.Data;

using FluentMigrator;

using Promomash.Common.Infra.Extensions;

namespace Promomash.Demo.Infra.Migrations
{
    [Tags("Development", "Testing", "Production")]
    [Migration(2021_12_09_02, "Creates users table")]
    public class Migration_2021_12_09_02_CreateUsersTable : BasePromomashDemoMigration
    {
        private readonly string _usersTableName = "Users";
        private readonly string _countriesTableName = "Countries";
        private readonly string _provincesTableName = "Provinces";
        public override void Up()
        {
            Create.Table(_usersTableName)
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("Login").AsString(255).NotNullable()
                .WithColumn("Password").AsString(255).NotNullable()
                .WithColumn("CountryId").AsInt64().NotNullable()
                .WithColumn("ProvinceId").AsInt64().NotNullable()
                .WithColumn("CreatedAt").AsDateTime2().NotNullable()
                .WithColumn("EditedAt").AsDateTime2().NotNullable()
                .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(false);

            Create.ForeignKey(table: _usersTableName, column: "CountryId", toTable: _countriesTableName, primaryColumn: "Id")
                .OnDelete(Rule.None);
            Create.ForeignKey(table: _usersTableName, column: "ProvinceId", toTable: _provincesTableName, primaryColumn: "Id")
                .OnDelete(Rule.None);

            Create.Index(table: _usersTableName, column: "Login").Unique();

            Create.UniqueConstraint("UK_Login")
                .OnTable(_usersTableName)
                .Column("Login");
        }

        public override void Down()
        {
            Delete.Index(table: _usersTableName, column: "Login");

            Create.UniqueConstraint("UK_Login")
                .OnTable(_usersTableName)
                .Column("Login");

            Delete.ForeignKey(table: _usersTableName, column: "CountryId", toTable: _countriesTableName);
            Delete.ForeignKey(table: _usersTableName, column: "ProvinceId", toTable: _provincesTableName);

            Delete.Table(_usersTableName);
        }
    }
}
