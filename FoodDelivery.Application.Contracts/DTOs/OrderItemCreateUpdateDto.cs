using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Application.Contracts.DTOs;

/// <summary>
/// DTO для создания или обновления позиции заказа
/// </summary>
/// <param name="OrderId">Идентификатор заказа</param>
/// <param name="DishId">Идентификатор блюда</param>
/// <param name="Quantity">Количество единиц блюда</param>
public record OrderItemCreateUpdateDto(
    [property: Range(1, int.MaxValue, ErrorMessage = "Идентификатор заказа должен быть положительным")]
    int OrderId,

    [property: Range(1, int.MaxValue, ErrorMessage = "Идентификатор блюда должен быть положительным")]
    int DishId,

    [property: Range(1, 100, ErrorMessage = "Количество блюд должно быть от 1 до 100.")]
    int Quantity
);