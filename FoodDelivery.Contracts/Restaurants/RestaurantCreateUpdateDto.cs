namespace FoodDelivery.Contracts.Restaurants;

/// <summary>
/// Данные для создания или обновления ресторана.
/// </summary>
public record RestaurantCreateUpdateDto(string Name, string Address, double Rating, TimeOnly OpenTime, TimeOnly CloseTime);