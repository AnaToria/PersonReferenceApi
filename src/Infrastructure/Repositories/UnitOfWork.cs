using Application.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public ICityRepository Cities { get; }
    public IPersonRepository Persons { get; }
    public IPhoneNumberRepository PhoneNumbers { get; }

    private readonly PersonReferenceDbContext _dbContext;

    public UnitOfWork(PersonReferenceDbContext dbContext)
    {
        _dbContext = dbContext;
        Cities = new CityRepository(dbContext);
        Persons = new PersonRepository(dbContext);
        PhoneNumbers = new PhoneNumberRepository(dbContext);
    }
    
    public Task<int> CommitAsync()
    {
        return _dbContext.SaveChangesAsync();
    }

    public void RejectChanges()
    {
        var entityEntries = _dbContext.ChangeTracker
            .Entries()
            .Where(e => e.State != EntityState.Unchanged);
        foreach (var entry in entityEntries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Modified:
                case EntityState.Deleted:
                    entry.Reload();
                    break;
            }
        }    
    }
}