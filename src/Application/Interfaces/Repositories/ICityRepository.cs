using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ICityRepository
{
    Task<City> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsWithIdAsync(int id, CancellationToken cancellationToken = default);
}