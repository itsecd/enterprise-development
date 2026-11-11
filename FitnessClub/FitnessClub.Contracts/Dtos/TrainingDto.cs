namespace FitnessClub.Contracts.Dtos;

/// <summary>
/// Данные записи на тренировку.
/// </summary>
public class TrainingDto
{
    /// <summary>
    /// ID записи.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID клиента.
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// ID тренера.
    /// </summary>
    public int TrainerId { get; set; }

    /// <summary>
    /// ID зала.
    /// </summary>
    public int HallId { get; set; }

    /// <summary>
    /// Дата и время начала тренировки.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Продолжительность в минутах.
    /// </summary>
    public int DurationMinutes { get; set; }

    /// <summary>
    /// Признак пробной тренировки.
    /// </summary>
    public bool IsTrial { get; set; }
}