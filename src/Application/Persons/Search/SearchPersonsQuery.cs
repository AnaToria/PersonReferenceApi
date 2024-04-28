using Application.Common.Wrappers.Query;
using Application.Persons.Models;
using Domain.Enums;

namespace Application.Persons.Search;

public class SearchPersonsQuery : Query<IEnumerable<PersonListItemDto>>
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public Gender? Gender { get; set; }
    public string? Pin { get; set; }
    public DateOnly? BirthDateFrom { get; set; }
    public DateOnly? BirthDateTo { get; set; }
    public int? CityId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}