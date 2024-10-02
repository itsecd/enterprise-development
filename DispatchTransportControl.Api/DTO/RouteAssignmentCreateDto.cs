using System.Text.Json.Serialization;

namespace DispatchTransportControl.Api.DTO;

/// <summary>
///     DTO для создания назначения водителя и транспортного средства на маршрут
/// </summary>
public class RouteAssignmentCreateDto
{
    /// <summary>
    ///     Уникальный идентификатор транспортного средства
    /// </summary>
    [JsonPropertyName("vehicle_id")]
    public required int VehicleId { get; set; }

    /// <summary>
    ///     Уникальный идентификатор водителя
    /// </summary>
    [JsonPropertyName("driver_id")]
    public required int DriverId { get; set; }

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