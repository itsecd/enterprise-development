using System.Text.Json.Serialization;

namespace DistrictEnterpriseStatisticalData.Api.DTO;

/// <summary>
/// DTO для поличества предприятий для каждого поставщика
/// </summary>
public class EnterpriseCountForSupplierDto
{
    /// <summary>
    /// Поставщик
    /// </summary>
    [JsonPropertyName(("supplier"))]
    public required SupplierDto Supplier { get; set; }
    
    /// <summary>
    /// Количество предприятий
    /// </summary>
    [JsonPropertyName(("enterprise_count"))]
    public required int EnterpriseCount { get; set; }
}