using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("Persons", "dbo");
        builder.HasKey(person => person.Id);
        builder.Property(person => person.Name)
            .HasMaxLength(50)
            .IsRequired()
            .IsUnicode();
        builder.Property(person => person.Surname)
            .HasMaxLength(50)
            .IsRequired()
            .IsUnicode();
        builder.Property(person => person.Gender)
            .HasMaxLength(10)
            .IsRequired();
        builder.Property(person => person.Pin)
            .HasMaxLength(11)
            .IsRequired();
        builder.Property(person => person.BirthDate)
            .IsRequired();
        builder.HasOne(person => person.City)
            .WithMany(city => city.Persons)
            .IsRequired();
    }
}