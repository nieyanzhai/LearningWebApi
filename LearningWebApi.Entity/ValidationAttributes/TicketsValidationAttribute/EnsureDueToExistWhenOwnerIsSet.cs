using System.ComponentModel.DataAnnotations;

namespace LearningWebApi.Api.Models.ValidationAttributes.TicketsValidationAttribute;

public class EnsureDueToExistWhenOwnerIsSet : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (validationContext.ObjectInstance is not Ticket ticket) return ValidationResult.Success;
        return ticket.ValidateDueToExistsWhenOwnerIsSet()
            ? ValidationResult.Success
            : new ValidationResult("DueTo must be set when owner is set");
    }
}