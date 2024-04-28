using Domain.Enums;

namespace Application.Persons.Models;

public class PersonListItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public string Pin { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Image { get; set; }
    public CityDto City { get; set; }
}