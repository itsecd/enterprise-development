namespace FoodDelivery.Contracts.Analytics;

/// <summary>
/// DTO ресторана с количеством оформленных в нём заказов.
/// </summary>
public record TopRestaurantDto(int RestaurantId, string Name, int OrderCount);