using Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class LocalizationConfiguration : IEntityTypeConfiguration<Localization>
{
    public void Configure(EntityTypeBuilder<Localization> builder)
    {
        builder.HasNoKey();
        
        builder.Property(localization => localization.ResourceKey)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(localization => localization.LanguageCode)
            .HasMaxLength(2)
            .IsRequired();
        
        builder.Property(localization => localization.LocalizedValue)
            .HasMaxLength(100)
            .IsUnicode()
            .IsRequired();
    }
}