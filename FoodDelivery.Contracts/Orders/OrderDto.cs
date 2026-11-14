namespace FoodDelivery.Contracts.Orders;

/// <summary>
/// DTO заказа для возврата клиенту.
/// </summary>
public record OrderDto(
    int Id,
    int ClientId,
    int RestaurantId,
    DateTime OrderDateTime,
    DateTime? DeliveryDateTime,
    decimal TotalPrice,
    IReadOnlyList<int> DishIds);