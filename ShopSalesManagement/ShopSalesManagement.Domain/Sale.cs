namespace ShopSalesManagement.Domain;

/// <summary>
/// Представляет продажу.
/// </summary>
public class Sale
{
    /// <summary>
    /// Уникальный идентификатор продажи.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Дата продажи.
    /// </summary>
    public DateTime SaleDate { get; set; }
    /// <summary>
    /// Идентификатор покупателя (по номеру карты).
    /// </summary>
    public int CustomerId { get; set; }
    /// <summary>
    /// Идентификатор магазина.
    /// </summary>
    public int StoreId { get; set; }
    /// <summary>
    /// Общая сумма продажи.
    /// </summary>
    public decimal TotalAmount { get; set; }

    public Sale(DateTime saleDate, int customerId, int storeId, decimal totalAmount)
    {
        SaleDate = saleDate;
        CustomerId = customerId;
        StoreId = storeId;
        TotalAmount = totalAmount;
    }
}