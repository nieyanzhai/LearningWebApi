using System.ComponentModel.DataAnnotations;

namespace LearningWebApi.Api.Models.ValidationAttributes.TicketsValidationAttribute;

public class EnsureDueToAfterNow : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (validationContext.ObjectInstance is not Ticket ticket) return ValidationResult.Success;
        return ticket.ValidateDueToAfterNow()
            ? ValidationResult.Success
            : new ValidationResult("DueTo date must be after now");
    }
}