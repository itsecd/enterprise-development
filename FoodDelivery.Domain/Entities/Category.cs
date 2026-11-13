namespace FoodDelivery.Domain.Entities;

/// <summary>
/// Категория блюда
/// </summary>
public class Category : IEntity
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