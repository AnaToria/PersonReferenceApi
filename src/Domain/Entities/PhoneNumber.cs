using Domain.Enums;

namespace Domain.Entities;

public class PhoneNumber
{
    public int Id { get; set; }
    public PhoneType Type { get; set; }
    public string Number { get; set; }
    public Person Person { get; set; }
}