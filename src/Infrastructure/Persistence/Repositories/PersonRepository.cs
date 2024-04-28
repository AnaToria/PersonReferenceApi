using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly PersonReferenceDbContext _dbContext;

    public PersonRepository(PersonReferenceDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<List<Person>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default) 
    {
       return _dbContext.Persons
           .Include(person => person.PhoneNumbers)
           .Paged(new Pagination(pageNumber, pageSize))
           .ToListAsync(cancellationToken);
    }

   public Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
   {
       return _dbContext.Persons
           .Include(person => person.PhoneNumbers)
           .Include(person => person.Relationships)
           .FirstOrDefaultAsync(person => person.Id == id && person.Status == EntityStatus.Active,
               cancellationToken);
   }

   public async Task AddAsync(Person entity, CancellationToken cancellationToken = default)
   { 
       await _dbContext.Persons.AddAsync(entity, cancellationToken);
   }

   public void Update(Person entity)
   {
       _dbContext.Persons.Update(entity);
   }

   public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
   {
       var person = await GetByIdAsync(id, cancellationToken);
       if (person is null)
           return;
       _dbContext.Persons.Remove(person);
   }

   public Task<bool> ExistsWithIdAsync(int id, CancellationToken cancellationToken = default)
   {
       return _dbContext.Persons
           .AnyAsync(person => person.Id == id && person.Status == EntityStatus.Active,
               cancellationToken);
   }

   public Task<bool> ExistsWithPinAsync(string pin, CancellationToken cancellationToken = default)
   {
       return _dbContext.Persons
           .AnyAsync(person => person.Pin == pin && person.Status == EntityStatus.Active,
               cancellationToken);
   }
}