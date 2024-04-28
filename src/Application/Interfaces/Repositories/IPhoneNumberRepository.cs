namespace Application.Interfaces.Repositories;

public interface IPhoneNumberRepository
{
    Task<bool> ExistsWithNumberAsync(string number, CancellationToken cancellationToken = default);
}