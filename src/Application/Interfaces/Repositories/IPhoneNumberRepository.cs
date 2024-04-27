using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IPhoneNumberRepository : IGenericRepository<PhoneNumber>
{
    Task<bool> ExistsWithNumberAsync(string number, CancellationToken cancellationToken = default);
}