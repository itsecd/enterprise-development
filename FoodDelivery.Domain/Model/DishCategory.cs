namespace FoodDelivery.Domain.Model;

/// <summary>
/// Справочник категорий блюд
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
    /// Список блюд принадлежащих данной категории
    /// </summary>
    public List<Dish> Dishes { get; set; } = [];
}
