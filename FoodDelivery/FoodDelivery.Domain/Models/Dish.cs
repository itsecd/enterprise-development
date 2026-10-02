namespace FoodDelivery.Domain.Models;

/// <summary>
/// Блюдо из меню ресторана
/// </summary>
public class Dish
{
    /// <summary>
    /// Идентификатор блюда
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
    /// Цена блюда
    /// </summary>
    public required decimal Price { get; set; }

    /// <summary>
    /// Категория блюда
    /// </summary>
    public required Category Category { get; set; }

    /// <summary>
    /// Ресторан, в котором приготовлено блюдо
    /// </summary>
    public required Restaurant Restaurant { get; set; }
}