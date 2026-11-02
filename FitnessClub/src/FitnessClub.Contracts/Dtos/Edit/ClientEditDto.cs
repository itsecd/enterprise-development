using System.ComponentModel.DataAnnotations;
using FitnessClub.Shared.Enums;

namespace FitnessClub.Contracts.Dtos.Edit;
/// <summary>
/// Данные для создания или изменения клиента
/// </summary>
public record ClientEditDto : IValidatableObject
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
    /// Начало абонемента
    /// </summary>
    public DateOnly SubscriptionStart { get; init; }
    /// <summary>
    /// Окончание абонемента
    /// </summary>
    public DateOnly SubscriptionEnd { get; init; }

    /// <summary>
    /// Проверяет даты рождения и абонемента
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

        if (SubscriptionStart == default || SubscriptionEnd == default)
        {
            yield return new ValidationResult(
                "Необходимо указать даты начала и окончания абонемента.",
                [nameof(SubscriptionStart), nameof(SubscriptionEnd)]);
        }

        if (SubscriptionStart > SubscriptionEnd)
        {
            yield return new ValidationResult(
                "Дата окончания абонемента не может быть меньше даты начала абонемента.",
                [nameof(SubscriptionStart), nameof(SubscriptionEnd)]);
        }
    }
}