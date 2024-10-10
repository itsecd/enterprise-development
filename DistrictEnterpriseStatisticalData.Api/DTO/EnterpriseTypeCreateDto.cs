using System.Text.Json.Serialization;

namespace DistrictEnterpriseStatisticalData.Api.DTO;

public class EnterpriseTypeCreateDto
{
    [JsonPropertyName("type")]
    public required string Type { get; set; }
}