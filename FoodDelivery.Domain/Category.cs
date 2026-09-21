namespace FoodDelivery.Domain;

/// <summary>
/// Категория блюд
/// </summary>
public class Category
{
    /// <summary>
    /// Уникальный идентификатор категории
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Название категории
    /// </summary>
    public required string Name { get; set; }
}