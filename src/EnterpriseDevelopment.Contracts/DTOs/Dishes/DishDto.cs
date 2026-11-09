namespace EnterpriseDevelopment.Contracts.DTOs.Dishes;

/// <summary>
/// Данные блюда
/// </summary>
public record DishDto(
    int Id,
    string Name,
    int Weight,
    decimal Price,
    int CategoryId,
    int RestaurantId);
