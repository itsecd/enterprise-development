using System.Text.Json.Serialization;

namespace DistrictEnterpriseStatisticalData.Api.DTO;

/// <summary>
/// DTO для создания поставки
/// </summary>
public class SupplyCreateDto
{
    /// <summary>
    ///     Количество поставляемого сырья
    /// </summary>
    [JsonPropertyName("quantity")]
    public required int Quantity { get; set; }

    /// <summary>
    ///     Дата поставки
    /// </summary>
    [JsonPropertyName("date")]
    public required DateOnly Date { get; set; }

    /// <summary>
    ///     Предприятие, которому осуществлялась поставка
    /// </summary>
    [JsonPropertyName("enterprise_registration_number")]
    public required int EnterpriseId { get; set; }

    /// <summary>
    ///     Поставщик
    /// </summary>
    [JsonPropertyName("supplier_id")]
    public required int SupplierId { get; set; }
}