namespace FoodDelivery.Domain;

/// <summary>
/// Заказ клиента в ресторане
/// </summary>
public class Order
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
    public required DateTime OrderTime { get; set; }

    /// <summary>
    /// Дата и время доставки заказа
    /// </summary>
    public required DateTime DeliveryTime { get; set; }

    /// <summary>
    /// Итоговая сумма заказа в рублях
    /// </summary>
    public required decimal TotalAmount { get; set; }

    /// <summary>
    /// Список позиций заказа
    /// </summary>
    public List<OrderItem> Items { get; set; } = [];
}