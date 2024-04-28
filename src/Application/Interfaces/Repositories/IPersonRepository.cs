using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Repositories;

public interface IPersonRepository : IGenericRepository<Person>
{
    Task<bool> ExistsWithPinAsync(string pin, CancellationToken cancellationToken = default);
    Task<IEnumerable<Person>> SearchAsync(string? name,
        string? surname, 
        string? pin,
        Gender? gender,
        DateTime? birthDateFrom,
        DateTime? birthDateTo,
        int? cityId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);
}