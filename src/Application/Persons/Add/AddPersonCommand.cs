using Application.Common.Wrappers.Command;
using Domain.Enums;

namespace Application.Persons.AddPerson;

public class AddPersonCommand : ICommand<int>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public string Pin { get; set; }
    public DateTime BirthDate { get; set; }
    public int CityId { get; set; }
    public List<PhoneNumberRequest> PhoneNumbers { get; set; }
}