using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork, IAsyncDisposable
{
    public ICityRepository Cities { get; }
    public IPersonRepository Persons { get; }
    public IPhoneNumberRepository PhoneNumbers { get; }

    private readonly PersonReferenceDbContext _dbContext;
    private IDbContextTransaction? _transaction;
    public UnitOfWork(PersonReferenceDbContext dbContext)
    {
        _dbContext = dbContext;
        Cities = new CityRepository(dbContext);
        Persons = new PersonRepository(dbContext);
        PhoneNumbers = new PhoneNumberRepository(dbContext);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        _transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
    }
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        return _transaction!.CommitAsync(cancellationToken);
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

    public void Dispose()
    {
        _dbContext.Dispose();
        _transaction?.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
        if (_transaction != null) await _transaction.DisposeAsync();
    }
}