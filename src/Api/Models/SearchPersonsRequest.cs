using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Api.Models;

public class SearchPersonsRequest
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public Gender? Gender { get; set; }
    public string? Pin { get; set; }
    public DateTime? BirthDateFrom { get; set; }
    public DateTime? BirthDateTo { get; set; }
    public int? CityId { get; set; }
    [Required]
    public int PageNumber { get; set; }
    [Required]
    public int PageSize { get; set; }
}