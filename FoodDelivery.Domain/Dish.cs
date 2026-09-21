namespace FoodDelivery.Domain;

/// <summary>
/// Блюдо из ресторана
/// </summary>
public class Dish
{
    /// <summary>
    /// Уникальный идентификатор блюда
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Название блюда
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Вес блюда в граммах
    /// </summary>
    public required int Weight { get; set; }

    /// <summary>
    /// Цена блюда в рублях
    /// </summary>
    public required decimal Price { get; set; }

    /// <summary>
    /// Идентификатор категории блюда
    /// </summary>
    public required int CategoryId { get; set; }

    /// <summary>
    /// Категория, к которой относится блюдо
    /// </summary>
    public required Category Category { get; set; }
}