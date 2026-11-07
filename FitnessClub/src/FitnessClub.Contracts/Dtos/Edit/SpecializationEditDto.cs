using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Dtos.Edit;
/// <summary>
/// Данные для создания или изменения специализации
/// </summary>
public record SpecializationEditDto : IValidatableObject
{
    /// <summary>
    /// Название специализации
    /// </summary>
    [Required, MaxLength(200)]
    public required string Name { get; init; }

    /// <summary>
    /// Проверяет, что название специализации указано
    /// </summary>
    /// <param name="validationContext">Контекст проверки</param>
    /// <returns>Ошибки валидации, если они есть</returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            yield return new ValidationResult(
                "Необходимо указать название специализации.",
                [nameof(Name)]);
        }
    }
}