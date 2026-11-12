namespace BeautySalon.Contracts.Analytics;

/// <summary>
/// Услуга и количество записей на неё
/// </summary>
public sealed class PopularServiceResponse
{
    /// <summary>
    /// Идентификатор услуги
    /// </summary>
    public required int ServiceId { get; init; }

    /// <summary>
    /// Название услуги
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Количество записей
    /// </summary>
    public required int AppointmentCount { get; init; }
}