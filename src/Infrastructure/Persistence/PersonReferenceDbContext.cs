using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class PersonReferenceDbContext : DbContext
{
    public PersonReferenceDbContext(DbContextOptions<PersonReferenceDbContext> options)
        : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    public DbSet<City> Cities => Set<City>();
    public DbSet<Person> Persons => Set<Person>();
    public DbSet<PhoneNumber> PhoneNumbers => Set<PhoneNumber>();
    public DbSet<PersonRelationship> PersonRelationships => Set<PersonRelationship>();
}