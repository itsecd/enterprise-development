using System.Text.Json.Serialization;

namespace DispatchTransportControl.Api.DTO;

/// <summary>
/// DTO для водителя и статистики по его поездкам
/// </summary>
public class DriverTripStatsDto
{
    /// <summary>
    /// Водитель
    /// </summary>
    [JsonPropertyName("driver")]
    public required DriverDto Driver { get; set; }

    /// <summary>
    /// Количество поездок
    /// </summary>
    [JsonPropertyName("trip_count")]
    public required int TripCount { get; set; }

    /// <summary>
    /// Среднее время поездки
    /// </summary>
    [JsonPropertyName("avg_time")]
    public required double AvgTime { get; set; }

    /// <summary>
    /// Максимальное время поездки
    /// </summary>
    [JsonPropertyName("max_time")]
    public required double MaxTime { get; set; }
}