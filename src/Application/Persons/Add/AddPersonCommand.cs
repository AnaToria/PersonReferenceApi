using Application.Common.Wrappers.Command;
using Domain.Enums;

namespace Application.Persons.Add;

public class AddPersonCommand : Command<int>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public string Pin { get; set; }
    public DateTime BirthDate { get; set; }
    public string Image { get; set; }
    public int CityId { get; set; }
    public List<PhoneNumberRequest> PhoneNumbers { get; set; }
}