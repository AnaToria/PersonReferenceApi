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
    
    public Task<List<Person>> GetAllAsync(int pageNumber, int pageSize, string? searchText, CancellationToken cancellationToken = default)
    {
        IQueryable<Person> query = _dbContext.Persons
            .Include(person => person.City);
        
       if (!string.IsNullOrEmpty(searchText))
       {
           string searchTextLower = searchText.ToLower();
           
           query = query.Where(person => person.Name.ToLower().Contains(searchTextLower)
                                                 || person.Surname.ToLower().Contains(searchTextLower)
                                                 || person.Pin.Contains(searchTextLower));
       }
       
       return query.Paged(pageNumber, pageSize)
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

    public async Task AddAsync(Person person, CancellationToken cancellationToken = default)
   { 
       await _dbContext.Persons.AddAsync(person, cancellationToken);
   }

    public void Update(Person person)
   {
       _dbContext.Persons.Update(person);
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

    public Task<List<Person>> SearchAsync(string? name, string? surname, string? pin, Gender? gender, DateOnly? birthDateFrom,
       DateOnly? birthDateTo, int? cityId, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
       var personsQueryable = _dbContext.Persons
           .Include(person => person.City)
           .AsQueryable();

       if (!string.IsNullOrEmpty(name))
           personsQueryable = personsQueryable.Where(person => 
               person.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase) 
               || person.Name.ToLower().Contains(name.ToLower()));

       if (!string.IsNullOrEmpty(surname))
           personsQueryable = personsQueryable.Where(person => 
               person.Surname.Equals(surname, StringComparison.CurrentCultureIgnoreCase) 
               || person.Surname.ToLower().Contains(surname.ToLower()));
       
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

       return personsQueryable
           .Paged(pageNumber, pageSize)
           .OrderBy(person => person.Id)
           .ToListAsync(cancellationToken);
    }

    public Task<List<Person>> GetAllWithRelationships(CancellationToken cancellationToken = default)
    {
        return _dbContext.Persons
            .Include(person => person.Relationships)
            .ToListAsync(cancellationToken);
    }
}