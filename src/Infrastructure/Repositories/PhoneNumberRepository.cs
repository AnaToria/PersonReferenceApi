using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PhoneNumberRepository : IPhoneNumberRepository
{
    private readonly PersonReferenceDbContext _dbContext;

    public PhoneNumberRepository(PersonReferenceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<PhoneNumber>> GetAllAsync(Pagination pagination, CancellationToken cancellationToken = default)
    {
        return _dbContext.PhoneNumbers
            .Paged(pagination)
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