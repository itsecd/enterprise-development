namespace FoodDelivery.Domain.Entities;

/// <summary>
/// Категория блюда.
/// </summary>
public class DishCategory : IEntity
{
    /// <summary>
    /// Идентификатор категории.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название категории.
    /// </summary>
    public required string Name { get; set; }
}