namespace FoodDelivery.Domain.Model;

/// <summary>
/// Блюдо из меню ресторана
/// </summary>
public class Dish
{
    /// <summary>
    /// Идентификатор блюда
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название блюда
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Вес блюда в граммах
    /// </summary>
    public required int Weight { get; set; }

    /// <summary>
    /// Цена блюда
    /// </summary>
    public required decimal Price { get; set; }

    /// <summary>
    /// Идентификатор категории блюда из справочника
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Категория блюда (справочник)
    /// </summary>
    public DishCategory? Category { get; set; }

    /// <summary>
    /// Идентификатор ресторана которому принадлежит блюдо
    /// </summary>
    public int RestaurantId { get; set; }

    /// <summary>
    /// Ресторан которому принадлежит блюдо
    /// </summary>
    public Restaurant? Restaurant { get; set; }

    /// <summary>
    /// Позиции заказов в которых есть блюдо
    /// </summary>
    public List<OrderItem> OrderItems { get; set; } = [];
}
