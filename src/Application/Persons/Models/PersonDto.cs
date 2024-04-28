using Application.Common.Constants;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Enums;

namespace Application.Persons.Models;

public class PersonDto
{
    public int Id { get;  set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public string Pin { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Image { get; set; }
    public CityDto City { get; set; }
    public List<PersonPhoneNumber> PhoneNumbers { get; set; }
    public List<PersonRelationshipDto> Relationships { get; set; }

    public static PersonDto Create(Person person, IImageService imageService, Language language)
    {
        var imageUrl = imageService.GetImageUrl(person.Image);

        var phoneNumbers = person.PhoneNumbers.Select(number => new PersonPhoneNumber
        {
            Number = number.Number,
            Type = number.Type
        }).ToList();

        var city = new CityDto
        {
            Id = person.City.Id,
            Name = language == Language.Ka ? person.City.Name : person.City.NameEn
        };

        var relationships = person.Relationships.Select(relationship => new PersonRelationshipDto
        {
            RelationshipType = relationship.RelationshipType,
            RelatedPerson = Create(relationship.RelatedPerson, imageService, language)
        }).ToList();

        return new PersonDto
        {
            Id = person.Id,
            Name = person.Name,
            Surname = person.Surname,
            BirthDate = person.BirthDate,
            Gender = person.Gender,
            Image = imageUrl,
            Pin = person.Pin,
            PhoneNumbers = phoneNumbers,
            City = city,
            Relationships = relationships
        };
    }
}