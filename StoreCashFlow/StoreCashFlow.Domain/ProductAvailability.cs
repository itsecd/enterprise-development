namespace StoreCashFlow.Domain;

/// <summary>
/// Наличие товара в магазине
/// </summary>
public class ProductAvailability
{
    /// <summary>
    /// Идентификатор наличия товара
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Магазин
    /// </summary>
    public required Store Store { get; set; }
    /// <summary>
    /// Товар
    /// </summary>
    public required Product Product { get; set; }
    /// <summary>
    /// Количество
    /// </summary>
    public required double Quantity { get; set; }
}
