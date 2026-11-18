namespace FitnessClub.Contracts.Dto;

/// <summary>
/// Занятие (read-модель)
/// </summary>
public class LessonDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Клиент
    /// </summary>
    public ClientDto Client { get; init; } = new();

    /// <summary>
    /// Тренер
    /// </summary>
    public TrainerDto Trainer { get; init; } = new();

    /// <summary>
    /// Дата и время начала
    /// </summary>
    public DateTime DateStart { get; init; }

    /// <summary>
    /// Дата и время окончания
    /// </summary>
    public DateTime? DateEnd { get; init; }

    /// <summary>
    /// Название зала
    /// </summary>
    public string? Hall { get; init; }

    /// <summary>
    /// Пробное занятие или нет
    /// </summary>
    public bool IsTrial { get; init; }
}