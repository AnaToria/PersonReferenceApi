namespace Domain.Entities;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string NameEn { get; set; }
    public List<Person>? Persons { get; set; }
}