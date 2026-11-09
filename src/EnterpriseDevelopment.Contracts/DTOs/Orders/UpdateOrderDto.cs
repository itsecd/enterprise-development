namespace EnterpriseDevelopment.Contracts.DTOs.Orders;

/// <summary>
/// Данные для обновления заказа
/// </summary>
public record UpdateOrderDto(
    int ClientId,
    int RestaurantId,
    List<int> DishIds,
    DateTimeOffset CreatedAt,
    DateTimeOffset DeliveredAt);
