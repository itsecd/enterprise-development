namespace EnterpriseDevelopment.Contracts.DTOs.Dishes;

/// <summary>
/// Данные для создания блюда
/// </summary>
public record CreateDishDto(
    string Name,
    int Weight,
    decimal Price,
    int CategoryId,
    int RestaurantId);
