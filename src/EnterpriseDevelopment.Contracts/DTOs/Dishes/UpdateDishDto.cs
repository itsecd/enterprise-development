namespace EnterpriseDevelopment.Contracts.DTOs.Dishes;

/// <summary>
/// Данные для обновления блюда
/// </summary>
public record UpdateDishDto(
    string Name,
    int Weight,
    decimal Price,
    int CategoryId,
    int RestaurantId);
