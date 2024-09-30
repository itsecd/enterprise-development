using System.Text.Json.Serialization;

namespace DispatchTransportControl.Api.DTO;

/// <summary>
/// DTO для периода времени
/// </summary>
public class TimePeriodDto
{
    /// <summary>
    /// Начало
    /// </summary>
    [JsonPropertyName("start")]
    public required DateTime Start { get; set; }

    /// <summary>
    /// Конец
    /// </summary>
    [JsonPropertyName("end")]
    public required DateTime End { get; set; }
}