using Domain.Enums;

namespace Domain.Entities;

public class PhoneNumber
{
    public int Id { get; set; }
    public PhoneType Type { get; set; }
    public string Number { get; set; }

    private PhoneNumber()
    {
        
    }

    public static PhoneNumber Create(PhoneType type, string number) =>
        new()
        {
            Type = type,
            Number = number
        };
}