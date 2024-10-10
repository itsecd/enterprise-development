using System.Text.Json.Serialization;

namespace DistrictEnterpriseStatisticalData.Api.DTO;

/// <summary>
/// DTO типа предприятия
/// </summary>
public class EnterpriseTypeDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    /// <summary>
    /// Тип предприятия
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; set; }
}