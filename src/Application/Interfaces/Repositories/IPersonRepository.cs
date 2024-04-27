using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IPersonRepository : IGenericRepository<Person>
{
    Task<bool> ExistsWithPinAsync(string pin, CancellationToken cancellationToken = default);
}