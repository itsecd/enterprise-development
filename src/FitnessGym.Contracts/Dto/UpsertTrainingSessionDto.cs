using System.ComponentModel.DataAnnotations;

namespace FitnessGym.Contracts.Dto;

/// <summary>
/// DTO записи клиента на персональное занятие.
/// </summary>
public class UpsertTrainingSessionDto : IValidatableObject
{
    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    [Required(ErrorMessage = "Не указан клиент")]
    [Range(0, int.MaxValue, ErrorMessage = "Некорректный идентификатор клиента")]
    public int? ClientId { get; init; }

    /// <summary>
    /// Идентификатор тренера.
    /// </summary>
    [Required(ErrorMessage = "Не указан тренер")]
    [Range(0, int.MaxValue, ErrorMessage = "Некорректный идентификатор тренера")]
    public int? TrainerId { get; init; }

    /// <summary>
    /// Дата и время начала занятия.
    /// </summary>
    [Required(ErrorMessage = "Не указано время начала занятия")]
    public DateTime? StartsAt { get; init; }

    /// <summary>
    /// Дата и время окончания занятия; может быть не проставлена.
    /// </summary>
    public DateTime? EndsAt { get; init; }

    /// <summary>
    /// Название зала, в котором проходит занятие.
    /// </summary>
    [Required(ErrorMessage = "Не указано название зала")]
    public string HallName { get; init; } = string.Empty;

    /// <summary>
    /// Является ли посещение пробным.
    /// </summary>
    public bool IsTrial { get; init; }

    /// <inheritdoc/>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndsAt <= StartsAt)
        {
            yield return new ValidationResult(
                "Время окончания занятия должно быть позже времени начала.",
                [nameof(EndsAt)]);
        }
    }
}
