using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Promomash.Common.Infra.EntityMappers;
using Promomash.Demo.Common.Entities;

namespace Promomash.Demo.Infra.EntityMappers
{
    /// <summary>
    /// User table mapper
    /// </summary>
    internal class UserMapper : AbstractEntityMapper<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users").HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Login).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.CountryId).IsRequired();
            builder.Property(x => x.ProvinceId).IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder
               .HasOne(x => x.Country)
               .WithMany()
               .HasForeignKey(x => x.CountryId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(x => x.Province)
               .WithMany()
               .HasForeignKey(x => x.ProvinceId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.Login).IsUnique();
        }
    }
}
