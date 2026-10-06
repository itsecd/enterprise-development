namespace FoodDelivery.Domain;
/// <summary>
/// Категория блюда
/// </summary>
public class Category
{
    /// <summary>
    /// ID категории
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// название категории
    /// </summary>
    public required string Name { get; set; }
}
