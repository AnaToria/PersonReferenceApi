using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class PhoneNumberRepository : IPhoneNumberRepository
{
    private readonly PersonReferenceDbContext _dbContext;

    public PhoneNumberRepository(PersonReferenceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<PhoneNumber>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return _dbContext.PhoneNumbers
            .Paged(new Pagination(pageNumber, pageSize))
            .ToListAsync(cancellationToken);
    }

    public Task<PhoneNumber?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _dbContext.PhoneNumbers
            .FirstOrDefaultAsync(phone => phone.Id == id, cancellationToken);
    }

    public async Task AddAsync(PhoneNumber entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.PhoneNumbers.AddAsync(entity, cancellationToken);
    }

    public void Update(PhoneNumber entity)
    {
        _dbContext.PhoneNumbers.Update(entity);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var phoneNumber = await GetByIdAsync(id, cancellationToken);
        if (phoneNumber is null)
            return;
        _dbContext.PhoneNumbers.Remove(phoneNumber);    
    }

    public Task<bool> ExistsWithIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _dbContext.PhoneNumbers.AnyAsync(phone => phone.Id == id, cancellationToken: cancellationToken);
    }
    
    public Task<bool> ExistsWithNumberAsync(string number, CancellationToken cancellationToken = default)
    {
        return _dbContext.PhoneNumbers.AnyAsync(phone => phone.Number == number, cancellationToken: cancellationToken);
    }
}