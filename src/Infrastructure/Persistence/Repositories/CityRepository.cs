using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class CityRepository : ICityRepository
{
    private readonly PersonReferenceDbContext _dbContext;

    public CityRepository(PersonReferenceDbContext dbContext) => _dbContext = dbContext;

    public Task<City?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
   {
       return _dbContext.Cities
           .FirstOrDefaultAsync(city => city.Id == id, cancellationToken);
   }

   public Task<bool> ExistsWithIdAsync(int id, CancellationToken cancellationToken = default)
   {
       return _dbContext.Cities.AnyAsync(city => city.Id == id, cancellationToken: cancellationToken);
   }
}