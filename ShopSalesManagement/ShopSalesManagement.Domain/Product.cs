namespace ShopSalesManagement.Domain;

/// <summary>
/// Представляет товар.
/// </summary>
public class Product
{
    /// <summary>
    /// Уникальный идентификатор товара.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Штрих-код товара.
    /// </summary>
    public string Barcode { get; set; }
    /// <summary>
    /// Идентификатор товарной группы.
    /// </summary>
    public int ProductGroupId { get; set; }
    /// <summary>
    /// Наименование товара.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Вес упаковки товара.
    /// </summary>
    public decimal Weight { get; set; }
    /// <summary>
    /// Тип товара (штучный или развесной).
    /// </summary>
    public string Type { get; set; }
    /// <summary>
    /// Цена товара.
    /// </summary>
    public decimal Price { get; set; }
    /// <summary>
    /// Предельная дата хранения товара.
    /// </summary>
    public DateTime ExpirationDate { get; set; }
}