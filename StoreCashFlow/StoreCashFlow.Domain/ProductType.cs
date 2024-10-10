namespace StoreCashFlow.Domain;

/// <summary>
/// Тип товара 
/// </summary>
public class ProductType
{
    /// <summary>
    /// Идентификатор типа товара
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Тип товара
    /// </summary>
    public required string Name { get; set; }
}

