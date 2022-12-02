using System.ComponentModel.DataAnnotations;
using LearningWebApi.Api.Models.ValidationAttributes.TicketsValidationAttribute;

namespace LearningWebApi.Api.Models;

public class Ticket
{
    [Key] [Required] public int Id { get; set; }
    [Required] [StringLength(50)] public string Title { get; set; }
    public string Description { get; set; }

    [StringLength(50)]
    public string? Owner { get; set; }

    [EnsureDueToExistWhenOwnerIsSet]
    [EnsureDueToAfterNow]
    public DateTime? DueTo { get; set; }

    [EnsureDueToAfterCreatedAt] public DateTime? CreatedAt { get; set; }
    public int ProjectId { get; set; }


    public bool ValidateDueToExistsWhenOwnerIsSet() => Owner == null || string.IsNullOrWhiteSpace(Owner) || DueTo != null;

    public bool ValidateDueToAfterNow() => DueTo == null || DueTo > DateTime.Now;

    public bool ValidateDueToAfterCreatedAt() => DueTo == null || CreatedAt == null || DueTo >= CreatedAt;
    
    public bool ValidateDescriptionExists() => !string.IsNullOrWhiteSpace(Description);
}