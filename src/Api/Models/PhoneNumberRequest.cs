using System.Text.Json.Serialization;
using Domain.Enums;

namespace Api.Models;

public class PhoneNumberRequest
{
    [JsonConverter(typeof(JsonStringEnumConverter))] 
    public PhoneType Type { get; set; }
    public string Number { get; set; }
}