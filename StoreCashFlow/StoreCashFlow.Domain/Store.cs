namespace StoreCashFlow.Domain;

/// <summary>
/// Магазин
/// </summary>
public class Store
{
    /// <summary>
    /// Идентификатор магазина
    /// </summary>
    public int StoreId { get; set; }
    /// <summary>
    /// Местоположение магазина
    /// </summary>
    public required string Location { get; set; }
}

