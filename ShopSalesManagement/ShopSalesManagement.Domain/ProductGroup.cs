namespace ShopSalesManagement.Domain;

/// <summary>
/// Представляет товарную группу.
/// </summary>
public class ProductGroup
{
    /// <summary>
    /// Уникальный идентификатор товарной группы.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Наименование товарной группы.
    /// </summary>
    public string Name { get; set; }

    public ProductGroup(string name)
    {
        Name = name;
    }
}