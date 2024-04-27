using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly PersonReferenceDbContext _dbContext;

    public PersonRepository(PersonReferenceDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<List<Person>> GetAllAsync(Pagination pagination, CancellationToken cancellationToken = default) 
    {
       return _dbContext.Persons
           .Include(person => person.PhoneNumbers)
           .Paged(pagination)
           .ToListAsync(cancellationToken);
    }

   public Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
   {
       return _dbContext.Persons
           .Include(person => person.PhoneNumbers)
           .FirstOrDefaultAsync(person => person.Id == id, cancellationToken);
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
       return _dbContext.Persons.AnyAsync(person => person.Id == id, cancellationToken);
   }

   public Task<bool> ExistsWithPinAsync(string pin, CancellationToken cancellationToken = default)
   {
       return _dbContext.Persons.AnyAsync(person => person.Pin == pin, cancellationToken: cancellationToken);
   }
}