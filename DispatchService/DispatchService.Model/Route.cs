namespace DispatchService.Model;

/// <summary>
/// Маршрут
/// </summary>
public class Route
{
    /// <summary>
    /// Номер маршрута
    /// </summary>
    public required string RouteNumber { get; set; }

    /// <summary>
    /// Назначенный транспорт
    /// </summary>
    public required Transport AssignedTransport { get; set; }

    /// <summary>
    /// Назначенный водитель
    /// </summary>
    public required Driver AssignedDriver { get; set; }

    /// <summary>
    /// Время начала маршрута 
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Время окончания маршрута
    /// </summary>
    public DateTime EndTime { get; set; }
}