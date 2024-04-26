using Domain.Entities;

namespace Domain.Interfaces;

public interface IPersonRepository : IGenericRepository<Person>
{
    Task<bool> ExistsWithPinAsync(string pin, CancellationToken cancellationToken = default);
}