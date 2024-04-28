using Domain.Enums;

namespace Domain.Entities;

public class Person : BaseEntity
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public Gender Gender { get; private set; }
    public string Pin { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public string Image { get; private set; }
    public City City { get; private set; }
    public List<PhoneNumber> PhoneNumbers { get; private set; }
    public List<PersonRelationship> Relationships { get; private set; }

    private Person()
    {
        PhoneNumbers = new List<PhoneNumber>();
        Relationships = new List<PersonRelationship>();
    }

    public static Person Create(string name, string surname, Gender gender, string pin, DateOnly birthDate,
        string image, City city, List<PhoneNumber> phoneNumbers, int? id = null) =>
        new()
        {
            Id = id ?? default,
            Name = name,
            Surname = surname,
            Gender = gender,
            Pin = pin,
            BirthDate = birthDate,
            Image = image,
            City = city,
            PhoneNumbers = phoneNumbers,
            Status = EntityStatus.Draft
        };

    public bool IsConnectedTo(Person person)
    {
        return Relationships
            .Any(relationship => relationship.RelatedPerson.Id == person.Id);
    }
    
    public void Connect(Person personToConnect, RelationshipType relationshipType)
    {
        var relationship = new PersonRelationship(relationshipType, this, personToConnect);
        
        Relationships.Add(relationship);
    }
    
    public void Disconnect(Person personToConnect)
    {
        var personRelationship = Relationships.First(relationship => relationship.RelatedPerson.Id == personToConnect.Id);
        
        Relationships.Remove(personRelationship);
    }
}