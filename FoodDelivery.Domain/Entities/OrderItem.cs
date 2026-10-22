namespace FoodDelivery.Domain.Entities;

/// <summary>
/// Отдельная позиция в заказе
/// </summary>
public class OrderItem : IEntity
{
    /// <summary>
    /// Уникальный идентификатор позиции заказа
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public required int OrderId { get; set; }

    /// <summary>
    /// Идентификатор блюда
    /// </summary>
    public required int DishId { get; set; }

    /// <summary>
    /// Блюдо, добавленное в заказ
    /// </summary>
    public required Dish Dish { get; set; }

    /// <summary>
    /// Количество единиц блюда
    /// </summary>
    public required int Quantity { get; set; }
}