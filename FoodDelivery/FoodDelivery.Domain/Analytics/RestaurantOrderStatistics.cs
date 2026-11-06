using FoodDelivery.Domain.Models;

namespace FoodDelivery.Domain.Analytics;

/// <summary>
/// Количество заказов ресторана
/// </summary>
/// <param name="Restaurant">Ресторан</param>
/// <param name="OrderCount">Количество заказов</param>
public sealed record RestaurantOrderStatistics(
    Restaurant Restaurant,
    int OrderCount);
