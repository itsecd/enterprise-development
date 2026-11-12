namespace FoodDelivery.Contracts.Dishes;

/// <summary>
/// Информация о блюде из меню ресторана.
/// </summary>
public record DishDto(int Id, string Name, int Weight, decimal Price, int CategoryId, int RestaurantId);