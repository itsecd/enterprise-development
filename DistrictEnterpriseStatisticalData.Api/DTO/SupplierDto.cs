using System.Text.Json.Serialization;

namespace DistrictEnterpriseStatisticalData.Api.DTO;

public class SupplierDto
{
    /// <summary>
    ///     Идентификатор поставщика
    /// </summary>
    [JsonPropertyName("id")]
    public required int Id { get; set; }

    /// <summary>
    ///     Имя поставщика
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }
}