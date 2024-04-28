using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories;

public interface IPersonRepository
{
    Task<List<Person>> GetAllAsync(int pageNumber, int pageSize, string? searchText, CancellationToken cancellationToken = default);
    Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Person>> SearchAsync(string? name,
        string? surname, 
        string? pin,
        Gender? gender,
        DateOnly? birthDateFrom,
        DateOnly? birthDateTo,
        int? cityId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);
    Task AddAsync(Person person, CancellationToken cancellationToken = default);
    void Update(Person person);
    Task<bool> ExistsWithIdAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsWithPinAsync(string pin, CancellationToken cancellationToken = default);
}