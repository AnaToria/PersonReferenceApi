using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
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
           .Include(person => person.City)
           .Include(person => person.Relationships)
           .ThenInclude(relationship => relationship.RelatedPerson)
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

   public async Task<IEnumerable<Person>> SearchAsync(string? name, string? surname, string? pin, Gender? gender, DateOnly? birthDateFrom,
       DateOnly? birthDateTo, int? cityId, int pageNumber, int pageSize, CancellationToken cancellationToken)
   {
       var personsQueryable = _dbContext.Persons
           .Include(person => person.City)
           .AsQueryable();

       if (!string.IsNullOrEmpty(name))
           personsQueryable = personsQueryable.Where(person => person.Name == name && person.Name.Contains(name));

       if (!string.IsNullOrEmpty(surname))
           personsQueryable = personsQueryable.Where(person => person.Surname == surname &&  person.Surname.Contains(surname));
       
       if (!string.IsNullOrEmpty(pin))
           personsQueryable = personsQueryable.Where(person => person.Pin == pin || person.Pin.Contains(pin));
       
       if (gender is not null)
           personsQueryable = personsQueryable.Where(person => person.Gender == gender);
       
       if (birthDateFrom is not null)
           personsQueryable = personsQueryable.Where(person => person.BirthDate > birthDateFrom);

       if (birthDateTo is not null)
           personsQueryable = personsQueryable.Where(person => person.BirthDate < birthDateTo);

       if (cityId is not null)
           personsQueryable = personsQueryable.Where(person => person.City.Id == cityId);

       return await personsQueryable
           .Paged(new Pagination(pageNumber, pageSize))
           .OrderBy(person => person.Id)
           .ToListAsync(cancellationToken);
   }
}