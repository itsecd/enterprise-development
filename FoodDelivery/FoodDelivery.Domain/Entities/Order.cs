using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Domain.Entities;

/// <summary>
/// Заказ на доставку еды
/// </summary>
[Table("Orders")]
public class Order
{
    /// <summary>
    /// Уникальный идентификатор заказа
    /// </summary>
    [Key]
    public int Id { get; set; }
    
    /// <summary>
    /// Идентификатор клиента, оформившего заказ
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// Клиент, оформивший заказ
    /// </summary>
    public required Customer Customer { get; set; }

    /// <summary>
    /// Идентификатор ресторана, готового исполнить заказ
    /// </summary>
    public int RestaurantId { get; set; }

    /// <summary>
    /// Ресторан, исполняющий заказ
    /// </summary>
    public required Restaurant Restaurant { get; set; }

    /// <summary>
    /// Идентификатор назначенного курьера
    /// </summary>
    public int? CourierId { get; set; }

    /// <summary>
    /// Назначенный курьер
    /// </summary>
    public required Courier? Courier { get; set; }

    /// <summary>
    /// Адрес доставки заказа
    /// </summary>
    public required string DeliveryAddress { get; set; }

    /// <summary>
    /// Общая сумма заказа в рублях
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Текущий статус заказа
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    /// Выбранный способ оплаты
    /// </summary>
    public PaymentMethod PaymentMethod { get; set; }

    /// <summary>
    /// Дата и время создания заказа
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Комментарий к заказу для курьера или кухни
    /// </summary>
    public string? Comment { get; set; }
}