namespace Museum.Domain;

/// <summary>
/// Экскурсия по музею
/// </summary>
public class Tour
{
    /// <summary>
    /// Идентификатор 
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Дата проведения 
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Время начала 
    /// </summary>
    public TimeSpan StartTime { get; set; }

    /// <summary>
    /// Продолжительность (мин)
    /// </summary>
    public int DurationMinutes { get; set; }

    /// <summary>
    /// Список выставок, которые посетят на экскурсии
    /// </summary>
    public List<Exhibition> Exhibitions { get; set; } = [];
}