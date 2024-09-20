namespace DispatchTransportControl.Domain;

/// <summary>
/// Назначение водителя и транспортного средства на маршрут
/// </summary>
public class RouteAssignment
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Транспортное средство
    /// </summary>
    public required Vehicle Vehicle { get; set; }

    /// <summary>
    /// Водитель
    /// </summary>
    public required Driver Driver { get; set; }

    /// <summary>
    /// Номер маршрута
    /// </summary>
    public required string RouteNumber { get; set; }

    /// <summary>
    /// Дата и время выхода на рейс
    /// </summary>
    public required DateTime StartTime { get; set; }

    /// <summary>
    /// Дата и время окончания рейса
    /// </summary>
    public required DateTime EndTime { get; set; }
}