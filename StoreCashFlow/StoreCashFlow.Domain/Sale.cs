namespace StoreCashFlow.Domain;

/// <summary>
/// Продажи товаров покупателям
/// </summary>
public class Sale
{
    /// <summary>
    /// Идентификатор покупки
    /// </summary>
    public int SaleId { get; set; }
    /// <summary>
    /// Магазин
    /// </summary>
    public required Store Store { get; set; }
    /// <summary>
    /// Продукт
    /// </summary>
    public required Product Product { get; set; }
    /// <summary>
    /// Количество
    /// </summary>
    public required double Quantity { get; set; }
    /// <summary>
    /// Дата продажи
    /// </summary>
    public required DateTime SaleDate { get; set; }
    /// <summary>
    /// Покупатель
    /// </summary>
    public required Customer Customer { get; set; }

}