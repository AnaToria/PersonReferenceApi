namespace Domain.Entities;

public class PersonRelationship
{
    private PersonRelationship() {}

    public PersonRelationship(RelationshipType relationshipType, Person person, Person relatedPerson)
    {
        RelationshipType = relationshipType;
        Person = person;
        RelatedPerson = relatedPerson;
    }

    public int Id { get; private set; }
    public RelationshipType RelationshipType { get; private set; }
    public Person Person { get; private set; }
    public Person RelatedPerson { get; private set; }
}