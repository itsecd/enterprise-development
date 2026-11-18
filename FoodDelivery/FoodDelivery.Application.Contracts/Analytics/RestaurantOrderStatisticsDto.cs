namespace FoodDelivery.Application.Contracts.Analytics;

/// <summary>
/// Количество заказов ресторана
/// </summary>
/// <param name="RestaurantId">Идентификатор ресторана</param>
/// <param name="RestaurantName">Название ресторана</param>
/// <param name="OrderCount">Количество заказов</param>
public sealed record RestaurantOrderStatisticsDto(
    int RestaurantId,
    string RestaurantName,
    int OrderCount);
