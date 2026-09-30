namespace EnterpriseDevelopment.Domain.Entities;

/// <summary>
/// Позиция заказа: блюдо и его количество.
/// </summary>
public class OrderItem
{
    /// <summary>
    /// Идентификатор позиции заказа.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор заказа.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Заказ.
    /// </summary>
    public Order? Order { get; set; }

    /// <summary>
    /// Идентификатор блюда.
    /// </summary>
    public int DishId { get; set; }

    /// <summary>
    /// Блюдо.
    /// </summary>
    public Dish? Dish { get; set; }

    /// <summary>
    /// Количество блюд.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Цена блюда на момент оформления заказа.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Стоимость позиции.
    /// </summary>
    public decimal TotalPrice => UnitPrice * Quantity;
}
