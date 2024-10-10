namespace EnterpriseWarehouse.Domain.Entities;
/// <summary>
/// Ячейка склада
/// </summary>
public class Cell
{
    /// <summary>
    /// Идентификатор ячейки
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Товар
    /// </summary>
    public Product? Product { get; set; }
    /// <summary>
    /// Количество товара в ячейке
    /// </summary>
    public required int Quantity { get; set; }
}
