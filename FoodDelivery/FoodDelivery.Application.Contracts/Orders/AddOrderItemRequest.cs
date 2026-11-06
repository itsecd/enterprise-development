using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Application.Contracts.Orders;

/// <summary>
/// Данные для добавления блюда в заказ
/// </summary>
public sealed class AddOrderItemRequest
{
    /// <summary>
    /// Идентификатор блюда
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Укажите положительный идентификатор блюда")]
    public required int DishId { get; init; }

    /// <summary>
    /// Количество блюд
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Количество должно быть положительным")]
    public required int Quantity { get; init; }
}
