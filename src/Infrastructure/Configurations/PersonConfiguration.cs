using Domain.Entities;
using Domain.Enums;
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
            .HasConversion(p => p.ToString(),
                p=>(Gender)Enum.Parse(typeof(Gender), p))
            .IsRequired();
        builder.Property(person => person.Pin)
            .HasMaxLength(11)
            .IsRequired();
        builder.Property(person => person.BirthDate)
            .IsRequired();
        builder.Property(person => person.CityId)
            .IsRequired();
        
        builder.HasOne(person => person.City)
            .WithMany(city => city.Persons)
            .IsRequired();
    }
}