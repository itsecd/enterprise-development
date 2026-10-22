namespace FoodDelivery.Application.Contracts.DTOs;

/// <summary>
/// DTO позиции заказа
/// </summary>
/// <param name="Id">Уникальный идентификатор позиции заказа</param>
/// <param name="OrderId">Идентификатор заказа</param>
/// <param name="DishId">Идентификатор блюда</param>
/// <param name="Quantity">Количество единиц блюда</param>
public record OrderItemDto(
    int Id,
    int OrderId,
    int DishId,
    int Quantity
);
