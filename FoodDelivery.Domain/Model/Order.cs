using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Domain.Model;

/// <summary>
/// Заказ на доставку еды
/// </summary>
public class Order
{
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Дата и время оформления заказа
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Время доставки заказа
    /// </summary>
    public DateTime? DeliveredAt { get; set; }

    /// <summary>
    /// Фактическая длительность доставки
    /// </summary>
    public TimeSpan? DeliveryDuration => DeliveredAt.HasValue ? DeliveredAt.Value - CreatedAt : null;

    /// <summary>
    /// Адрес доставки
    /// </summary>
    public required string DeliveryAddress { get; set; }

    /// <summary>
    /// Статус заказа
    /// </summary>
    public OrderStatus Status { get; set; } = OrderStatus.Created;

    /// <summary>
    /// Идентификатор клиента оформившего заказ
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// Клиент оформивший заказ
    /// </summary>
    public Customer? Customer { get; set; }

    /// <summary>
    /// Идентификатор ресторана в котором оформлен заказ
    /// </summary>
    public int RestaurantId { get; set; }

    /// <summary>
    /// Ресторан в котором оформлен заказ
    /// </summary>
    public Restaurant? Restaurant { get; set; }

    /// <summary>
    /// Идентификатор курьера доставляющего заказ
    /// </summary>
    public int? CourierId { get; set; }

    /// <summary>
    /// Курьер доставляющий заказ
    /// </summary>
    public Courier? Courier { get; set; }

    /// <summary>
    /// Блюда в заказе
    /// </summary>
    public List<OrderItem> Items { get; set; } = [];

    /// <summary>
    /// Способ оплаты
    /// </summary>
    public required PaymentMethod PaymentMethod { get; set; }

    /// <summary>
    /// Стоимость доставки
    /// </summary>
    public required decimal DeliveryFee { get; set; }

    /// <summary>
    /// Итоговая сумма заказа
    /// </summary>
    public decimal Total { get; set; }
}
