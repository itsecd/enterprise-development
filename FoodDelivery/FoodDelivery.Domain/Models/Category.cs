namespace FoodDelivery.Domain.Models;

/// <summary>
/// Категория блюд
/// </summary>
public class Category
{
    /// <summary>
    /// Идентификатор категории
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Название категории
    /// </summary>
    public required string Name { get; set; }
}