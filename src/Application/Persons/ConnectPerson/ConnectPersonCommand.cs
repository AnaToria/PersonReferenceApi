using Application.Common.Wrappers.Command;
using Domain.Entities;
using Domain.Enums;

namespace Application.Persons.ConnectPerson;

public class ConnectPersonCommand : Command
{
    public int PersonId { get; set; }
    public int PersonIdToConnectWith { get; set; }
    public RelationshipType RelationshipType { get; set; }
}