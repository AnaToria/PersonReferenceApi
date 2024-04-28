using Domain.Enums;

namespace Application.Reports.Models;

public class PersonRelationshipsByTypeDto
{
    public  RelationshipType RelationshipType { get; set; }
    public int? Count { get; set; }
}