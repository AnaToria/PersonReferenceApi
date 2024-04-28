using Domain.Enums;

namespace Application.Reports.Models;

public class PersonReportListItemDto
{
    public int Id { get;  set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public string Pin { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Image { get; set; }
    public List<PersonRelationshipsByTypeDto> PersonRelationshipsByTypes { get; set; }
}