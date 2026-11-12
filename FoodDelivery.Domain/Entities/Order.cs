namespace FoodDelivery.Domain.Entities;

/// <summary>
/// Заказ клиента.
/// </summary>
public class Order : IEntity
{
    /// <summary>
    /// Идентификатор заказа.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// Клиент, оформивший заказ.
    /// </summary>
    public Client? Client { get; set; }

    /// <summary>
    /// Идентификатор ресторана.
    /// </summary>
    public int RestaurantId { get; set; }

    /// <summary>
    /// Ресторан, в котором оформлен заказ.
    /// </summary>
    public Restaurant? Restaurant { get; set; }

    /// <summary>
    /// Время оформления заказа.
    /// </summary>
    public required DateTime OrderDateTime { get; set; }

    /// <summary>
    /// Время доставки заказа.
    /// </summary>
    public DateTime? DeliveryDateTime { get; set; }

    /// <summary>
    /// Итоговая стоимость заказа.
    /// </summary>
    public required decimal TotalPrice { get; set; }

    /// <summary>
    /// Список блюд в заказе.
    /// </summary>
    public List<Dish> Dishes { get; set; } = [];
}