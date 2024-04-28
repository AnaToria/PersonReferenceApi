using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.Entities;
using Domain.Enums;

namespace Api.Models;

public class DisconnectPersonRequest
{
    [Required]
    public int PersonId { get; set; }
    [Required]
    public int PersonIdToRemoveConnectionWith { get; set; }
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))] 
    public RelationshipType RelationshipType { get; set; }
}