namespace FoodDelivery.Application.Contracts.DTOs;

/// <summary>
/// DTO заказа
/// </summary>
/// <param name="Id">Уникальный идентификатор заказа</param>
/// <param name="CustomerId">Идентификатор клиента</param>
/// <param name="RestaurantId">Идентификатор ресторана</param>
/// <param name="CreatedAt">Дата и время создания заказа</param>
/// <param name="DeliveredAt">Дата и время доставки заказа</param>
/// <param name="TotalAmount">Общая стоимость заказа</param>
/// <param name="Items">Список позиций заказа</param>
public record OrderDto(
    int Id,
    int CustomerId,
    int RestaurantId,
    DateTime CreatedAt,
    DateTime DeliveredAt,
    decimal TotalAmount,
    List<OrderItemDto> Items
);
