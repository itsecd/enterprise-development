namespace Museum.Contracts.Dto;

/// <summary>
/// Хранит информацию об экскурсии для ответа API
/// </summary>
public sealed class TourDto
{
    /// <summary>
    /// Идентификатор экскурсии
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Дата проведения экскурсии
    /// </summary>
    public DateTime Date { get; init; }

    /// <summary>
    /// Время начала экскурсии
    /// </summary>
    public TimeSpan StartTime { get; init; }

    /// <summary>
    /// Продолжительность экскурсии в минутах
    /// </summary>
    public int DurationMinutes { get; init; }

    /// <summary>
    /// Выставки, которые входят в экскурсию
    /// </summary>
    public IReadOnlyList<ExhibitionDto> Exhibitions { get; init; } = [];
}
