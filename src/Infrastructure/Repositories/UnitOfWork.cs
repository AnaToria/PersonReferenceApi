using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public ICityRepository Cities { get; }

    private readonly PersonReferenceDbContext _dbContext;

    public UnitOfWork(PersonReferenceDbContext dbContext)
    {
        _dbContext = dbContext;
        Cities = new CityRepository(dbContext);
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
        }    }
}