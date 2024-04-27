using Application.Common.Wrappers.Command;

namespace Application.Persons.Delete;

public class DeletePersonCommand : Command<bool>
{
    public int Id { get; set; }
}