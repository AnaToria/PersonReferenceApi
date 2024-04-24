namespace Domain.Interfaces;

public interface IUnitOfWork
{
    ICityRepository Cities { get; }

    Task<int> CommitAsync();
    void RejectChanges();
}