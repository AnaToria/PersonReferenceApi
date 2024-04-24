using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
{
    public void Configure(EntityTypeBuilder<PhoneNumber> builder)
    {
        builder.ToTable("PhoneNumbers", "dbo");
        builder.HasKey(phoneNumber => phoneNumber.Id);
        builder.Property(phoneNumber => phoneNumber.Type)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(phoneNumber => phoneNumber.Number)
            .HasMaxLength(50)
            .IsRequired();
        builder.HasOne(phoneNumber => phoneNumber.Person)
            .WithMany(person => person.PhoneNumbers)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}