using System.Text.Json.Serialization;

namespace DistrictEnterpriseStatisticalData.Api.DTO;

/// <summary>
/// DTO для создания поставщика
/// </summary>
public class SupplierCreateDto
{
    /// <summary>
    ///     Имя поставщика
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }
}