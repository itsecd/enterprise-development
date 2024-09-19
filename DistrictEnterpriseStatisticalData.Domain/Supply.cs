namespace DistrictEnterpriseStatisticalData.Domain;

/// <summary>
///     Поставка
/// </summary>
public class Supply
{
    /// <summary>
    ///     Идентификатор поставки
    /// </summary>
    public int SupplyId { get; set; }

    /// <summary>
    ///     Количество поставляемого сырья
    /// </summary>
    public required int Quantity { get; set; }

    /// <summary>
    ///     Дата поставки
    /// </summary>
    public required DateOnly Date { get; set; }
}