using System.Text.Json.Serialization;

namespace DistrictEnterpriseStatisticalData.Api.DTO;

/// <summary>
/// DTO формы собственности
/// </summary>
public class FormOfOwnershipDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    /// <summary>
    /// Форма собственности
    /// </summary>
    [JsonPropertyName("form")]
    public required string Form { get; set; }
}