using System.Text.Json.Serialization;
using DistrictEnterpriseStatisticalData.Domain.Entity;

namespace DistrictEnterpriseStatisticalData.Api.DTO;

public class SupplierCountForTypeAndFormDto
{
    [JsonPropertyName(("form_of_property"))]
    public required FormOfOwnership Form { get; set; }
    
    [JsonPropertyName(("enterprise_type"))]
    public required EnterpriseType Type { get; set; }
    
    [JsonPropertyName(("supplier_count"))]
    public required int SupplierCount { get; set; }
}