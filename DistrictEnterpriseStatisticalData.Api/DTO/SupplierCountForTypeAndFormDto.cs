using System.Text.Json.Serialization;
using DistrictEnterpriseStatisticalData.Domain.Entity;

namespace DistrictEnterpriseStatisticalData.Api.DTO;

/// <summary>
/// DTO для поставщиков для каждого типа предприятия и формы собственности
/// </summary>
public class SupplierCountForTypeAndFormDto
{
    /// <summary>
    /// Форма собственности
    /// </summary>
    [JsonPropertyName(("form_of_property"))]
    public required FormOfOwnership Form { get; set; }
    
    /// <summary>
    /// Тип предприятия
    /// </summary>
    [JsonPropertyName(("enterprise_type"))]
    public required EnterpriseType Type { get; set; }
    
    /// <summary>
    /// Количество поставщиков
    /// </summary>
    [JsonPropertyName(("supplier_count"))]
    public required int SupplierCount { get; set; }
}