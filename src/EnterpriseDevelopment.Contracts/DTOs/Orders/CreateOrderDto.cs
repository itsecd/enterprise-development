namespace EnterpriseDevelopment.Contracts.DTOs.Orders;

/// <summary>
/// Данные для создания заказа
/// </summary>
public record CreateOrderDto(
    int ClientId,
    int RestaurantId,
    List<int> DishIds,
    DateTimeOffset CreatedAt,
    DateTimeOffset DeliveredAt);
