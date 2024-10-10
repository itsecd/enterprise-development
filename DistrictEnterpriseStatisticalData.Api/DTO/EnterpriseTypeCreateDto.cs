using System.Text.Json.Serialization;

namespace DistrictEnterpriseStatisticalData.Api.DTO;

/// <summary>
/// DTO для создания типа предприятия
/// </summary>
public class EnterpriseTypeCreateDto
{
    /// <summary>
    /// Тип предприятия
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; set; }
}