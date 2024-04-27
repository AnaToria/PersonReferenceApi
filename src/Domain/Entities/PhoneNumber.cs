using Domain.Enums;

namespace Domain.Entities;

public class PhoneNumber
{
    public int Id { get; private set; }
    public PhoneType Type { get; private  set; }
    public string Number { get; private set; }

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