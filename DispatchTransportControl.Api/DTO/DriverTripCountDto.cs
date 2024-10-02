using System.Text.Json.Serialization;

namespace DispatchTransportControl.Api.DTO;

/// <summary>
///     DTO для водителя и его количества поездок
/// </summary>
public class DriverTripCountDto
{
    /// <summary>
    ///     Водитель
    /// </summary>
    [JsonPropertyName("driver")]
    public required DriverDto Driver { get; set; }

    /// <summary>
    ///     Количество поездок
    /// </summary>
    [JsonPropertyName("trip_count")]
    public required int TripCount { get; set; }
}