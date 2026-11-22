using System.ComponentModel.DataAnnotations;
using BeautySalon.Shared.Enums;

namespace BeautySalon.Contracts.Dto;

/// <summary>
/// Клиент салона (для создания и изменения)
/// </summary>
public record ClientEditDto : IValidatableObject
{
    /// <summary>
    /// Полное имя
    /// </summary>
    [Required, MaxLength(200)]
    public required string FullName { get; init; }

    /// <summary>
    /// Пол
    /// </summary>
    [EnumDataType(typeof(Gender))]
    public required Gender Gender { get; init; }

    /// <summary>
    /// Дата рождения (должна быть в прошлом)
    /// </summary>
    public required DateOnly BirthDate { get; init; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    [Required, Phone, MaxLength(20)]
    public required string PhoneNumber { get; init; }

    /// <inheritdoc />
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (BirthDate >= DateOnly.FromDateTime(DateTime.Today))
        {
            yield return new ValidationResult(
                "Дата рождения должна быть в прошлом.",
                [nameof(BirthDate)]);
        }
    }
}
