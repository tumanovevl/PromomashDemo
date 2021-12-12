using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Promomash.Common.Infra.EntityMappers;
using Promomash.Demo.Common.Entities;

namespace Promomash.Demo.Infra.EntityMappers
{
    /// <summary>
    /// Province table mapper
    /// </summary>
    internal class ProvinceMapper : AbstractEntityMapper<Province>
    {
        public override void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.ToTable("Provinces").HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.CountryId).IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder
               .HasOne(x => x.Country)
               .WithMany()
               .HasForeignKey(x => x.CountryId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.Title);
        }
    }
}
