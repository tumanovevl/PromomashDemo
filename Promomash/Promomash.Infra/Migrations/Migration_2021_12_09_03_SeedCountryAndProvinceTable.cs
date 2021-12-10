using System;
using System.Collections.Generic;
using System.Data;

using FluentMigrator;

using Promomash.Common.Infra.Extensions;

namespace Promomash.Demo.Infra.Migrations
{
    [Tags("Development", "Testing")]
    [Migration(2021_12_09_03, "Seed country and province table")]
    public class Migration_2021_12_09_03_SeedCountryAndProvinceTable : BasePromomashDemoMigration
    {
        private readonly string _countriesTableName = "Countries";
        private readonly string _provincesTableName = "Provinces";
        private readonly DateTime _rowDate = DateTime.Parse("2021-09-12 19:00:00.0000000");
        public override void Up()
        {
            Execute.Sql($"SET IDENTITY_INSERT dbo.{_countriesTableName} ON");
            foreach (var item in GetSeedDataCountries())
            {
                Insert.IntoTable(_countriesTableName).Row(item);
            }
            Execute.Sql($"SET IDENTITY_INSERT dbo.{_countriesTableName} OFF");

            Execute.Sql($"SET IDENTITY_INSERT dbo.{_provincesTableName} ON");
            foreach (var item in GetSeedDataProvinces())
            {
                Insert.IntoTable(_provincesTableName).Row(item);
            }
            Execute.Sql($"SET IDENTITY_INSERT dbo.{_provincesTableName} OFF");
        }

        public override void Down()
        {
            Execute.Sql($"ALTER TABLE dbo.{_provincesTableName} NOCHECK CONSTRAINT ALL");
            foreach (var item in GetSeedDataProvinces())
            {
                Delete.FromTable(_provincesTableName).Row(item);
            }
            Execute.Sql($"ALTER TABLE dbo.{_provincesTableName} WITH CHECK CHECK CONSTRAINT ALL");

            Execute.Sql($"ALTER TABLE dbo.{_countriesTableName} NOCHECK CONSTRAINT ALL");
            foreach (var item in GetSeedDataCountries())
            {
                Delete.FromTable(_countriesTableName).Row(item);
            }
            Execute.Sql($"ALTER TABLE dbo.{_countriesTableName} WITH CHECK CHECK CONSTRAINT ALL");
        }

        private IEnumerable<object> GetSeedDataCountries()
        {
            yield return new { Id = 1, Title = "Country 1", CreatedAt = _rowDate, EditedAt = _rowDate, IsDeleted = false };
            yield return new { Id = 2, Title = "Country 2", CreatedAt = _rowDate, EditedAt = _rowDate, IsDeleted = false };
        }

        private IEnumerable<object> GetSeedDataProvinces()
        {
            yield return new { Id = 1, Title = "Province 1.1", CountryId = 1, CreatedAt = _rowDate, EditedAt = _rowDate, IsDeleted = false };
            yield return new { Id = 2, Title = "Province 1.2", CountryId = 1, CreatedAt = _rowDate, EditedAt = _rowDate, IsDeleted = false };
            yield return new { Id = 3, Title = "Province 1.3", CountryId = 1, CreatedAt = _rowDate, EditedAt = _rowDate, IsDeleted = false };
            yield return new { Id = 4, Title = "Province 2.1", CountryId = 2, CreatedAt = _rowDate, EditedAt = _rowDate, IsDeleted = false };
            yield return new { Id = 5, Title = "Province 2.2", CountryId = 2, CreatedAt = _rowDate, EditedAt = _rowDate, IsDeleted = false };
            yield return new { Id = 6, Title = "Province 2.3", CountryId = 2, CreatedAt = _rowDate, EditedAt = _rowDate, IsDeleted = false };
        }
    }
}
