using Domain.Enums;

namespace Domain.Entities;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public string Pin { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Image { get; set; }
    public City City { get; set; }
    public List<PhoneNumber> PhoneNumbers { get; set; }

    private Person()
    {
        PhoneNumbers = new List<PhoneNumber>();
    }

    public static Person Create(string name, string surname, Gender gender, string pin, DateTime birthDate,
        City city, List<PhoneNumber> phoneNumbers, int? id = null) =>
        new()
        {
            Id = id ?? default,
            Name = name,
            Surname = surname,
            Gender = gender,
            Pin = pin,
            BirthDate = birthDate,
            City = city,
            PhoneNumbers = phoneNumbers
        };
}