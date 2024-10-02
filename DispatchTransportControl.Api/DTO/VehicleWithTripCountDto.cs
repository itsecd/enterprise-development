using System.Text.Json.Serialization;

namespace DispatchTransportControl.Api.DTO;

/// <summary>
///     DTO для транспортного средства и количества поездок
/// </summary>
public class VehicleWithTripCountDto
{
    /// <summary>
    ///     Транспортное средство
    /// </summary>
    [JsonPropertyName("vehicle")]
    public required VehicleDto Vehicle { get; set; }

    /// <summary>
    ///     Количество поездок
    /// </summary>
    [JsonPropertyName("trip_count")]
    public required int TripCount { get; set; }
}