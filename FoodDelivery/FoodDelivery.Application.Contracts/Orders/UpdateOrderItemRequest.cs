using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Application.Contracts.Orders;

/// <summary>
/// Данные для изменения количества в позиции заказа
/// </summary>
public sealed class UpdateOrderItemRequest
{
    /// <summary>
    /// Новое количество блюд
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Количество должно быть положительным")]
    public required int Quantity { get; init; }
}
