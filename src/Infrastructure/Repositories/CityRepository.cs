using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CityRepository : ICityRepository
{
    private readonly PersonReferenceDbContext _dbContext;

    public CityRepository(PersonReferenceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<City>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Cities.ToListAsync(cancellationToken);
    }
}