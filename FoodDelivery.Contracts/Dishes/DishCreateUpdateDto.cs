namespace FoodDelivery.Contracts.Dishes;

/// <summary>
/// Данные для создания или обновления блюда.
/// </summary>
public record DishCreateUpdateDto(string Name, int Weight, decimal Price, int CategoryId, int RestaurantId);