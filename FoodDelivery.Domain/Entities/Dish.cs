namespace FoodDelivery.Domain.Entities;

/// <summary>
/// Блюдо ресторана.
/// </summary>
public class Dish : IEntity
{
    /// <summary>
    /// Идентификатор блюда.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название блюда.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Вес блюда в граммах.
    /// </summary>
    public required int Weight { get; set; }

    /// <summary>
    /// Цена блюда.
    /// </summary>
    public required decimal Price { get; set; }

    /// <summary>
    /// Идентификатор категории блюда.
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Категория блюда.
    /// </summary>
    public DishCategory? Category { get; set; }

    /// <summary>
    /// Идентификатор ресторана.
    /// </summary>
    public int RestaurantId { get; set; }

    /// <summary>
    /// Ресторан, которому принадлежит блюдо.
    /// </summary>
    public Restaurant? Restaurant { get; set; }
}