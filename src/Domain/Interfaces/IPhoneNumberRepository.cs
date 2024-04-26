using Domain.Entities;

namespace Domain.Interfaces;

public interface IPhoneNumberRepository : IGenericRepository<PhoneNumber>
{
    Task<bool> ExistsWithNumberAsync(string number, CancellationToken cancellationToken = default);
}