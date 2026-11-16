using EnterpriseDevelopment.Domain.Entities;

namespace EnterpriseDevelopment.Infrastructure.Persistence;

/// <summary>
/// Позиция заказа для хранения количества блюд
/// </summary>
public class OrderItem
{
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Заказ
    /// </summary>
    public Order Order { get; set; } = null!;

    /// <summary>
    /// Идентификатор блюда
    /// </summary>
    public int DishId { get; set; }

    /// <summary>
    /// Блюдо
    /// </summary>
    public Dish Dish { get; set; } = null!;

    /// <summary>
    /// Количество блюд
    /// </summary>
    public int Quantity { get; set; }
}
