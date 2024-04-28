using Application.Common.Wrappers.Query;
using Application.Persons.Models;

namespace Application.Persons.GetById;

public class GetByIdQuery : Query<PersonDto>
{
    public int Id { get; set; }
}