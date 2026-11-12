namespace BeautySalon.Contracts.Analytics;

/// <summary>
/// Количество повторных записей за отчётный месяц
/// </summary>
public sealed class RepeatAppointmentsResponse
{
    /// <summary>
    /// Начало периода включительно
    /// </summary>
    public required DateTime From { get; init; }

    /// <summary>
    /// Конец периода без включения границы
    /// </summary>
    public required DateTime To { get; init; }

    /// <summary>
    /// Количество повторных записей
    /// </summary>
    public required int Count { get; init; }
}