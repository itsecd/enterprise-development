namespace FoodDelivery.Application.Contracts.Orders;

/// <summary>
/// Сведения о позиции заказа
/// </summary>
public sealed class OrderItemDto
{
    /// <summary>
    /// Идентификатор позиции внутри заказа
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Идентификатор блюда
    /// </summary>
    public required int DishId { get; init; }

    /// <summary>
    /// Количество блюд
    /// </summary>
    public required int Quantity { get; init; }

    /// <summary>
    /// Цена единицы блюда на момент добавления в заказ
    /// </summary>
    public required decimal UnitPrice { get; init; }

    /// <summary>
    /// Стоимость позиции с учётом количества
    /// </summary>
    public required decimal TotalAmount { get; init; }
}
