using Domain.Enums;

namespace Application.Persons.Models;

public class PersonPhoneNumber
{
    public int? Id { get; set; }
    public PhoneType Type { get; set; }
    public string Number { get; set; }
}