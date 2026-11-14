namespace FoodDelivery.Contracts.Restaurants;

/// <summary>
/// Информация о ресторане-партнёре.
/// </summary>
public record RestaurantDto(int Id, string Name, string Address, double Rating, TimeOnly OpenTime, TimeOnly CloseTime);