namespace EnterpriseDevelopment.Application.DTOs;

/// <summary>
/// Данные ресторана с количеством оформленных заказов
/// </summary>
public record TopRestaurantDto(
    int RestaurantId,
    string RestaurantName,
    int OrderCount);
