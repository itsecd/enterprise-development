using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Validation;

/// <summary>Проверяет порядок двух дат в DTO.</summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class DateNotBeforeAttribute(string otherProperty) : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var start = validationContext.ObjectType.GetProperty(otherProperty)?.GetValue(validationContext.ObjectInstance);
        return value is DateTime end && start is DateTime begin && end < begin
            ? new ValidationResult("Дата окончания должна быть не раньше даты начала.",
                [validationContext.MemberName ?? validationContext.DisplayName])
            : ValidationResult.Success;
    }
}
