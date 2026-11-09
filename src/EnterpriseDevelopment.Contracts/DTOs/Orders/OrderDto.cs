namespace EnterpriseDevelopment.Contracts.DTOs.Orders;

/// <summary>
/// Данные заказа
/// </summary>
public record OrderDto(
    int Id,
    int ClientId,
    int RestaurantId,
    List<int> DishIds,
    DateTimeOffset CreatedAt,
    DateTimeOffset DeliveredAt,
    decimal TotalAmount,
    TimeSpan? DeliveryTime);
