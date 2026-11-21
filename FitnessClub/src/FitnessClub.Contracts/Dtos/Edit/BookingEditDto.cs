using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Dtos.Edit;
/// <summary>
/// Данные для создания или изменения бронирования
/// </summary>
public record BookingEditDto : IValidatableObject
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    [Range(1, int.MaxValue)]
    public required int ClientId { get; init; }
    /// <summary>
    /// Идентификатор тренера
    /// </summary>
    [Range(1, int.MaxValue)]
    public required int TrainerId { get; init; }
    /// <summary>
    /// Идентификатор зала
    /// </summary>
    [Range(1, int.MaxValue)]
    public required int HallId { get; init; }
    /// <summary>
    /// Дата и время тренировки
    /// </summary>
    public required DateTime DateTime { get; init; }
    /// <summary>
    /// Пробная ли тренировка
    /// </summary>
    public bool IsTrial { get; init; }
    /// <summary>
    /// Длительность тренировки
    /// </summary>
    [Range(typeof(TimeSpan), "00:30:00", "06:00:00")]
    public required TimeSpan Duration { get; init; }

    /// <summary>
    /// Проверяет корректность даты тренировки
    /// </summary>
    /// <param name="validationContext">Контекст проверки</param>
    /// <returns>Ошибки валидации, если они есть</returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DateTime == default)
        {
            yield return new ValidationResult(
                "Необходимо указать дату и время тренировки.",
                [nameof(DateTime)]);
        }
    }
}