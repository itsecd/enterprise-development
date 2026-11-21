namespace TransportCompany.Domain.Enums;

/// <summary>
/// Статус рейса
/// </summary>
public enum TripStatus
{
    /// <summary>
    /// Запланирован
    /// </summary>
    Planned = 0,

    /// <summary>
    /// В пути
    /// </summary>
    InTransit = 1,

    /// <summary>
    /// Завершён
    /// </summary>
    Completed = 2
}