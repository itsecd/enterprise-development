using System.Text.Json.Serialization;

namespace DistrictEnterpriseStatisticalData.Api.DTO;

public class FormOfOwnershipCreateDto
{
    [JsonPropertyName("form")]
    public required string Form { get; set; }
}