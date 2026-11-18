namespace FoodDelivery.Domain.Models;

/// <summary>
/// Блюдо в составе заказа
/// </summary>
public sealed class OrderItem
{
    /// <summary>
    /// Идентификатор позиции внутри заказа
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Блюдо в позиции заказа
    /// </summary>
    public required Dish Dish { get; init; }

    /// <summary>
    /// Количество блюд
    /// </summary>
    public required int Quantity { get; set; }

    /// <summary>
    /// Цена единицы блюда на момент добавления в заказ
    /// </summary>
    public required decimal UnitPrice { get; init; }
}
