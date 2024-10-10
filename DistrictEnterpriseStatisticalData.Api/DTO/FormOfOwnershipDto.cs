using System.Text.Json.Serialization;

namespace DistrictEnterpriseStatisticalData.Api.DTO;

public class FormOfOwnershipDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("form")]
    public required string Form { get; set; }
}