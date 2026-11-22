namespace EnterpriseDevelopment.Contracts.DTOs.Analytics;

/// <summary>
/// Данные ресторана с количеством оформленных заказов
/// </summary>
/// <param name="RestaurantId">Идентификатор ресторана</param>
/// <param name="RestaurantName">Название ресторана</param>
/// <param name="OrderCount">Количество оформленных заказов</param>
public record TopRestaurantDto(
    int RestaurantId,
    string RestaurantName,
    int OrderCount);
