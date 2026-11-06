namespace TransportCompany.Domain.Shared.Enums;

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
    /// В рейсе
    /// </summary>
    InProgress = 1,

    /// <summary>
    /// Завершён
    /// </summary>
    Completed = 2
}