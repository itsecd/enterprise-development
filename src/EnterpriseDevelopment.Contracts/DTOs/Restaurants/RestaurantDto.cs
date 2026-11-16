namespace EnterpriseDevelopment.Contracts.DTOs.Restaurants;

/// <summary>
/// Данные ресторана
/// </summary>
public record RestaurantDto(
    int Id,
    string Name,
    string Address,
    double Rating,
    TimeOnly OpeningTime,
    TimeOnly ClosingTime);
