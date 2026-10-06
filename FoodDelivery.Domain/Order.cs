using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Domain;
/// <summary>
/// Класс заказа
/// </summary>
public class Order
{
    /// <summary>
    /// ID заказа
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Время приготовления
    /// </summary>
    public DateTime CookedAt { get; set; }
    /// <summary>
    /// Время  доставки
    /// </summary>
    public DateTime? DeliveredAt { get; set; }
    /// <summary>
    /// ID клиента
    /// </summary>
    public int ClientId { get; set; }
    /// <summary>
    /// Ссылка на клиента
    /// </summary>
    public required Client Client { get; set; }
    /// <summary>
    /// ID ресторана
    /// </summary>
    public int RestaurantId { get; set; }
    /// <summary>
    /// Ссылка на ресторан
    /// </summary>
    public required Restaurant Restaurant { get; set; }
    /// <summary>
    /// Список заказанных позиций
    /// </summary>
    public List<OrderItem> Items { get; set; } = [];
    /// <summary>
    /// Метод подсчёта суммы заказа
    /// </summary>
    public decimal TotalAmount => Items.Sum(item => item.Dish.Price * item.Quantity);

}
