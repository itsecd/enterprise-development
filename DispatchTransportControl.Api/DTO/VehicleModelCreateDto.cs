using System.Text.Json.Serialization;

namespace DispatchTransportControl.Api.DTO;

/// <summary>
///     DTO для создания модели транспортного средства
/// </summary>
public class VehicleModelCreateDto
{
    /// <summary>
    ///     Название модели
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    ///     Низкопольная модель или нет
    /// </summary>
    [JsonPropertyName("low_floor")]
    public required bool LowFloor { get; set; }

    /// <summary>
    ///     Максимальная вместимость
    /// </summary>
    [JsonPropertyName("max_capacity")]
    public required int MaxCapacity { get; set; }
}