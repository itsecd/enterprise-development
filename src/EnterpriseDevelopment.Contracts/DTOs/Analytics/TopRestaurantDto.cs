namespace EnterpriseDevelopment.Contracts.DTOs.Analytics;

/// <summary>
/// Данные ресторана с количеством оформленных заказов
/// </summary>
public record TopRestaurantDto(
    int RestaurantId,
    string RestaurantName,
    int OrderCount);
