using Application.Common.Wrappers.Command;

namespace Application.Persons.DisconnectPerson;

public class DisconnectPersonCommand : Command
{
    public int PersonId { get; set; }
    public int PersonIdToRemoveConnectionWith { get; set; }
}