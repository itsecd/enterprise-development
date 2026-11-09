namespace EnterpriseDevelopment.Contracts.DTOs.Restaurants;

/// <summary>
/// Данные для обновления ресторана
/// </summary>
public record UpdateRestaurantDto(
    string Name,
    string Address,
    double Rating,
    TimeOnly OpeningTime,
    TimeOnly ClosingTime);
