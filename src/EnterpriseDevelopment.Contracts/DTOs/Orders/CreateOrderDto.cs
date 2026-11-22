namespace EnterpriseDevelopment.Contracts.DTOs.Orders;

/// <summary>
/// Данные для создания заказа
/// </summary>
/// <param name="ClientId">Идентификатор клиента</param>
/// <param name="RestaurantId">Идентификатор ресторана</param>
/// <param name="DishIds">Список идентификаторов блюд в заказе</param>
/// <param name="CreatedAt">Дата и время создания заказа</param>
/// <param name="DeliveredAt">Дата и время доставки заказа</param>
public record CreateOrderDto(
    int ClientId,
    int RestaurantId,
    List<int> DishIds,
    DateTimeOffset CreatedAt,
    DateTimeOffset DeliveredAt);
