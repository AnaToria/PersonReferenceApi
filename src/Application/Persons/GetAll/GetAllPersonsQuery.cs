using Application.Common.Wrappers.Query;
using Application.Persons.Models;

namespace Application.Persons.GetAll;

public class GetAllPersonsQuery : Query<IEnumerable<PersonListItemDto>>
{
    public string? SearchText { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}