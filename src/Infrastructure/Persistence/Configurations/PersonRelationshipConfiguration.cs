using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class PersonRelationshipConfiguration : IEntityTypeConfiguration<PersonRelationship>
{
    public void Configure(EntityTypeBuilder<PersonRelationship> builder)
    {
        builder.ToTable("PersonRelationships", "dbo");
        
        builder.HasKey(relationship => relationship.Id);
        
        builder.Property(relationship => relationship.RelationshipType)
            .HasConversion(type => type.ToString(),
                type=> (RelationshipType)Enum.Parse(typeof(RelationshipType), type))
            .IsRequired();

        builder.HasOne(relationship => relationship.Person)
            .WithMany(person => person.Relationships)
            .IsRequired();

        builder.HasOne(relationship => relationship.RelatedPerson)
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}