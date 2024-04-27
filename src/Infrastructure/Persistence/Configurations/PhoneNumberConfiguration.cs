using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
{
    public void Configure(EntityTypeBuilder<PhoneNumber> builder)
    {
        builder.ToTable("PhoneNumbers", "dbo");
        
        builder.HasKey(phoneNumber => phoneNumber.Id);
        
        builder.Property(phoneNumber => phoneNumber.Type)
            .HasConversion(p => p.ToString(),
                p=>(PhoneType)Enum.Parse(typeof(PhoneType), p))
            .IsRequired();
        
        builder.Property(phoneNumber => phoneNumber.Number)
            .HasMaxLength(50)
            .IsRequired();
    }
}