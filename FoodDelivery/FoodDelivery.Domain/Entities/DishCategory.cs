namespace FoodDelivery.Domain.Entities;

/// <summary>
/// Категории блюда в меню
/// </summary>
public class DishCategory
{
    /// <summary>
    /// Идентификатор категории
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Наименование категории
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Блюда данной категории
    /// </summary>
    public ICollection<Dish> Dishes { get; set; } = [];
}