using System.Text.Json.Serialization;

namespace DistrictEnterpriseStatisticalData.Api.DTO;

/// <summary>
/// DTO поставки
/// </summary>
public class SupplyDto
{
    /// <summary>
    ///     Идентификатор поставки
    /// </summary>
    [JsonPropertyName("supply_id")]
    public int SupplyId { get; set; }

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
    public virtual required EnterpriseDto Enterprise { get; set; }

    /// <summary>
    ///     Поставщик
    /// </summary>
    [JsonPropertyName("supplier_id")]
    public virtual required SupplierDto Supplier { get; set; }
}