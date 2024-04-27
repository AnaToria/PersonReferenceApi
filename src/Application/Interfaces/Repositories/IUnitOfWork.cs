namespace Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    ICityRepository Cities { get; }
    IPersonRepository Persons { get; }
    IPhoneNumberRepository PhoneNumbers { get; }
    
    Task<int> CommitAsync();
    void RejectChanges();
}