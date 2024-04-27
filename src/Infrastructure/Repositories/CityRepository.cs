using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CityRepository : ICityRepository
{
    private readonly PersonReferenceDbContext _dbContext;

    public CityRepository(PersonReferenceDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
   public Task<List<City>> GetAllAsync(Pagination pagination, CancellationToken cancellationToken = default)
   {
       return _dbContext.Cities
           .Paged(pagination)
           .ToListAsync(cancellationToken);
   }

   public Task<City?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
   {
       return _dbContext.Cities
           .FirstOrDefaultAsync(city => city.Id == id, cancellationToken);
   }

   public async Task AddAsync(City entity, CancellationToken cancellationToken = default)
   { 
       await _dbContext.Cities.AddAsync(entity, cancellationToken);
   }

   public void Update(City entity)
   {
       _dbContext.Cities.Update(entity);
   }

   public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
   {
       var city = await GetByIdAsync(id, cancellationToken);
       if (city is null)
           return;
       _dbContext.Cities.Remove(city);
   }

   public Task<bool> ExistsWithIdAsync(int id, CancellationToken cancellationToken = default)
   {
       return _dbContext.Cities.AnyAsync(city => city.Id == id, cancellationToken: cancellationToken);
   }
}