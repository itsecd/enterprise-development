using System.Text.Json.Serialization;

namespace DistrictEnterpriseStatisticalData.Api.DTO;

public class EnterpriseCountForSupplierDto
{
    [JsonPropertyName(("supplier"))]
    public required SupplierDto Supplier { get; set; }
    
    [JsonPropertyName(("enterprise_count"))]
    public required int EnterpriseCount { get; set; }
}