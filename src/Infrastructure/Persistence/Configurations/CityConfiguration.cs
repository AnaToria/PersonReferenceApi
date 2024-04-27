using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities", "dbo");
        builder.HasKey(city => city.Id);
        builder.Property(city => city.Name)
            .HasMaxLength(100)
            .IsRequired()
            .IsUnicode();
        builder.Property(city => city.NameEn)
            .HasMaxLength(100)
            .IsRequired()
            .IsUnicode();
    }
}