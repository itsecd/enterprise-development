namespace EnterpriseDevelopment.Contracts.DTOs.Restaurants;

/// <summary>
/// Данные для создания ресторана
/// </summary>
public record CreateRestaurantDto(
    string Name,
    string Address,
    double Rating,
    TimeOnly OpeningTime,
    TimeOnly ClosingTime);
