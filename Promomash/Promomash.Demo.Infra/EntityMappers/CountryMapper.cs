using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Promomash.Common.Infra.EntityMappers;
using Promomash.Demo.Common.Entities;

namespace Promomash.Demo.Infra.EntityMappers
{
    /// <summary>
    /// Country table mapper
    /// </summary>
    internal class CountryMapper : AbstractEntityMapper<Country>
    {
        public override void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries").HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.HasIndex(x => x.Title).IsUnique();
        }
    }
}
