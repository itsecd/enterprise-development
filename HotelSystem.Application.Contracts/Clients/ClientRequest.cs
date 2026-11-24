using System.ComponentModel.DataAnnotations;

namespace HotelSystem.Application.Contracts.Clients;

/// <summary>
/// Данные клиента для создания и обновления
/// </summary>
public class ClientRequest : IValidatableObject
{
    /// <summary>
    /// Номер паспорта
    /// </summary>
    [Required]
    [StringLength(20)]
    public required string PassportNumber { get; init; }

    /// <summary>
    /// Фамилия
    /// </summary>
    [Required]
    [StringLength(100)]
    public required string LastName { get; init; }

    /// <summary>
    /// Имя
    /// </summary>
    [Required]
    [StringLength(100)]
    public required string FirstName { get; init; }

    /// <summary>
    /// Отчество
    /// </summary>
    [StringLength(100)]
    public string? Patronymic { get; init; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public required DateTime BirthDate { get; init; }

    /// <summary>
    /// Гражданство
    /// </summary>
    [Required]
    [StringLength(100)]
    public required string Citizenship { get; init; }

    /// <inheritdoc />
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (BirthDate.Date > DateTime.Today)
        {
            yield return new ValidationResult(
                "Дата рождения не может быть в будущем",
                [nameof(BirthDate)]);
        }

        if (BirthDate.Year < 1900)
        {
            yield return new ValidationResult(
                "Дата рождения не может быть раньше 1900 года",
                [nameof(BirthDate)]);
        }
    }
}