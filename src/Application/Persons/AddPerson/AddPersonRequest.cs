using System.Text.Json.Serialization;
using Domain.Enums;

namespace Application.Persons.AddPerson;

public class AddPersonRequest
{
    public string Name { get; set; }
    public string Surname { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))] 
    public Gender Gender { get; set; }
    public string Pin { get; set; }
    public DateTime BirthDate { get; set; }
    public int CityId { get; set; }
    public List<PhoneNumberRequest> PhoneNumbers { get; set; }
}