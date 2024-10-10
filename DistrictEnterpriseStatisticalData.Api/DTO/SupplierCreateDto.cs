using System.Text.Json.Serialization;

namespace DistrictEnterpriseStatisticalData.Api.DTO;

public class SupplierCreateDto
{
    /// <summary>
    ///     Имя поставщика
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }
}