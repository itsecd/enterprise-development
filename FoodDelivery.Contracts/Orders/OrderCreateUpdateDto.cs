namespace FoodDelivery.Contracts.Orders;

/// <summary>
/// DTO для создания или обновления заказа.
/// </summary>
public record OrderCreateUpdateDto(
    int ClientId,
    int RestaurantId,
    DateTime OrderDateTime,
    DateTime? DeliveryDateTime,
    decimal TotalPrice,
    IReadOnlyList<int> DishIds);