using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class PhoneNumberRepository : IPhoneNumberRepository
{
    private readonly PersonReferenceDbContext _dbContext;

    public PhoneNumberRepository(PersonReferenceDbContext dbContext) => _dbContext = dbContext;

    public Task<PhoneNumber?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _dbContext.PhoneNumbers
            .FirstOrDefaultAsync(phone => phone.Id == id, cancellationToken);
    }
    
    public Task<bool> ExistsWithNumberAsync(string number, CancellationToken cancellationToken = default)
    {
        return _dbContext.PhoneNumbers.AnyAsync(phone => phone.Number == number, cancellationToken: cancellationToken);
    }
}