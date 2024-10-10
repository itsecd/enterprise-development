using System.Text.Json.Serialization;

namespace DistrictEnterpriseStatisticalData.Api.DTO;

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
    public virtual required SupplierDto Supplier { get; set; }
}