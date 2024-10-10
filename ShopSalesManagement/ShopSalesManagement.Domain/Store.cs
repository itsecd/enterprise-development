namespace ShopSalesManagement.Domain;

/// <summary>
/// Представляет магазин.
/// </summary>
public class Store
{
    /// <summary>
    /// Уникальный идентификатор магазина.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Название магазина.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Адрес магазина.
    /// </summary>
    public string Address { get; set; }
}