using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class GetAllPersonsRequest
{
    public string? SearchText { get; set; }
    [Required]
    public int PageNumber { get; set; }
    [Required]
    public int PageSize { get; set; }
}