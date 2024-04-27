using Domain.Enums;

namespace Domain.Entities;

public class Person
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public Gender Gender { get; private set; }
    public string Pin { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string? Image { get; private set; }
    public City City { get; private set; }
    public List<PhoneNumber> PhoneNumbers { get; private set; }
    public List<PersonRelationship> Relationships { get; private set; }

    private Person()
    {
        PhoneNumbers = new List<PhoneNumber>();
        Relationships = new List<PersonRelationship>();
    }

    public static Person Create(string name, string surname, Gender gender, string pin, DateTime birthDate,
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
            PhoneNumbers = phoneNumbers
        };
}