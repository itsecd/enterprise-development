namespace FoodDelivery.Domain.Models;

/// <summary>
/// Блюдо в составе заказа
/// </summary>
public sealed class OrderItem
{
    /// <summary>Идентификатор заказа</summary>
    public int OrderId { get; set; }

    /// <summary>Идентификатор блюда</summary>
    public int DishId { get; set; }

    /// <summary>
    /// Идентификатор позиции
    /// </summary>
    public required int Id { get; set; }

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
