namespace EnterpriseDevelopment.Contracts.DTOs.Orders;

/// <summary>
/// Данные заказа
/// </summary>
/// <param name="Id">Идентификатор заказа</param>
/// <param name="ClientId">Идентификатор клиента</param>
/// <param name="RestaurantId">Идентификатор ресторана</param>
/// <param name="DishIds">Список идентификаторов блюд в заказе</param>
/// <param name="CreatedAt">Дата и время создания заказа</param>
/// <param name="DeliveredAt">Дата и время доставки заказа</param>
/// <param name="TotalAmount">Общая стоимость заказа</param>
/// <param name="DeliveryTime">Продолжительность доставки заказа, если она определена</param>
public record OrderDto(
    int Id,
    int ClientId,
    int RestaurantId,
    List<int> DishIds,
    DateTimeOffset CreatedAt,
    DateTimeOffset DeliveredAt,
    decimal TotalAmount,
    TimeSpan? DeliveryTime);
