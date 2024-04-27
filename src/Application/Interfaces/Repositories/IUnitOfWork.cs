namespace Application.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    ICityRepository Cities { get; }
    IPersonRepository Persons { get; }
    IPhoneNumberRepository PhoneNumbers { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task BeginTransactionAsync(CancellationToken cancellationToken);
    Task CommitTransactionAsync(CancellationToken cancellationToken);
    void RejectChanges();
}