using System.Text.Json.Serialization;
using Domain.Enums;

namespace Application.Persons;

public class PhoneNumberRequest
{
    [JsonConverter(typeof(JsonStringEnumConverter))] 
    public PhoneType Type { get; set; }
    public string Number { get; set; }
}