using System.ComponentModel.DataAnnotations;

namespace LearningWebApi.Api.Models.ValidationAttributes.TicketsValidationAttribute;

public class EnsureDueToAfterCreatedAt : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (validationContext.ObjectInstance is not Ticket ticket) return ValidationResult.Success;
        return ticket.ValidateDueToAfterCreatedAt()
            ? ValidationResult.Success
            : new ValidationResult("DueTo must be after CreatedAt");
    }
}