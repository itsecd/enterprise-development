namespace FoodDelivery.Domain.Entities;

/// <summary>
/// Заказ клиента в ресторане
/// </summary>
public class Order : IEntity
{
    /// <summary>
    /// Уникальный идентификатор заказа
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Идентификатор клиента, оформившего заказ
    /// </summary>
    public required int CustomerId { get; set; }

    /// <summary>
    /// Клиент, оформивший заказ
    /// </summary>
    public required Customer Customer { get; set; }

    /// <summary>
    /// Идентификатор ресторана, в котором оформлен заказ
    /// </summary>
    public required int RestaurantId { get; set; }

    /// <summary>
    /// Ресторан, в котором оформлен заказ
    /// </summary>
    public required Restaurant Restaurant { get; set; }

    /// <summary>
    /// Дата и время оформления заказа
    /// </summary>
    public required DateTime CreatedAt { get; set; }

    /// <summary>
    /// Дата и время доставки заказа
    /// </summary>
    public required DateTime DeliveredAt { get; set; }

    /// <summary>
    /// Итоговая сумма заказа в рублях
    /// </summary>
    public decimal TotalAmount => Items.Sum(item => item.Dish.Price * item.Quantity);

    /// <summary>
    /// Список позиций заказа
    /// </summary>
    public List<OrderItem> Items { get; set; } = new();
}