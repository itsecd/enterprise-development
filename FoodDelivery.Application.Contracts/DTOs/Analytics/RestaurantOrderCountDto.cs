namespace FoodDelivery.Application.Contracts.DTOs.Analytics;

/// <summary>
/// Содержит информацию о ресторане и количестве его заказов
/// </summary>
/// <param name="RestaurantId">Идентификатор ресторана</param>
/// <param name="RestaurantName">Название ресторана</param>
/// <param name="OrderCount">Количество заказов ресторана</param>
public record RestaurantOrderCountDto(
    int RestaurantId,
    string RestaurantName,
    int OrderCount);