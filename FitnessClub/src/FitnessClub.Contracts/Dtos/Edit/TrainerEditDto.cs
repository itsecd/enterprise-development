using System.ComponentModel.DataAnnotations;
using FitnessClub.Shared.Enums;

namespace FitnessClub.Contracts.Dtos.Edit;
/// <summary>
/// Данные для создания или изменения тренера
/// </summary>
public record TrainerEditDto : IValidatableObject
{
    /// <summary>
    /// Номер паспорта
    /// </summary>
    [Required, RegularExpression(@"^\d{4} \d{6}$")]
    public required string PassportNumber { get; init; }
    /// <summary>
    /// Полное имя
    /// </summary>
    [Required, MaxLength(200)]
    public required string FullName { get; init; }
    /// <summary>
    /// Пол
    /// </summary>
    [EnumDataType(typeof(Gender))]
    public Gender Gender { get; init; }
    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateOnly BirthDate { get; init; }
    /// <summary>
    /// Номер телефона
    /// </summary>
    [Phone, MaxLength(30)]
    public string? PhoneNumber { get; init; }
    /// <summary>
    /// Опыт работы в годиках
    /// </summary>
    [Range(0, 60)]
    public int Experience { get; init; }
    /// <summary>
    /// Идентификатор специализации
    /// </summary>
    [Range(1, int.MaxValue)]
    public int SpecializationId { get; init; }

    /// <summary>
    /// Проверяет дату рождения
    /// </summary>
    /// <param name="validationContext">
    /// Контекст проверки
    /// </param>
    /// <returns>
    /// Ошибки валидации, если они есть
    /// </returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (BirthDate == default || BirthDate >= DateOnly.FromDateTime(DateTime.Today))
        {
            yield return new ValidationResult(
                "Дата рождения должна быть указана и находиться в прошлом.",
                [nameof(BirthDate)]);
        }
    }
}