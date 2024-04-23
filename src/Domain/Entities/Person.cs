namespace Domain.Entities;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Gender { get; set; }
    public string Pin { get; set; }
    public DateTime BirthDate { get; set; }
    public City City { get; set; }
    public List<PhoneNumber> PhoneNumbers { get; set; }
}