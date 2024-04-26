using Domain.Models;

namespace Domain.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<List<T>> GetAllAsync(Pagination pagination, CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    void Update(T entity);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsWithIdAsync(int id, CancellationToken cancellationToken = default);
}