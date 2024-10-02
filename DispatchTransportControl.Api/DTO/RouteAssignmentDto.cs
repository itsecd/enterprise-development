using System.Text.Json.Serialization;

namespace DispatchTransportControl.Api.DTO;

/// <summary>
///     DTO для назначения водителя и транспортного средства на маршрут
/// </summary>
public class RouteAssignmentDto
{
    /// <summary>
    ///     Уникальный идентификатор
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    ///     Транспортное средство
    /// </summary>
    [JsonPropertyName("vehicle")]
    public required VehicleDto Vehicle { get; set; }

    /// <summary>
    ///     Водитель
    /// </summary>
    [JsonPropertyName("driver")]
    public required DriverDto Driver { get; set; }

    /// <summary>
    ///     Номер маршрута
    /// </summary>
    [JsonPropertyName("route_number")]
    public required string RouteNumber { get; set; }

    /// <summary>
    ///     Дата и время выхода на рейс
    /// </summary>
    [JsonPropertyName("start_time")]
    public required DateTime StartTime { get; set; }

    /// <summary>
    ///     Дата и время окончания рейса
    /// </summary>
    [JsonPropertyName("end_time")]
    public required DateTime EndTime { get; set; }
}