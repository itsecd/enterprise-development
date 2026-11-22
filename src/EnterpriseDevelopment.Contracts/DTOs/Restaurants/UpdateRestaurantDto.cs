namespace EnterpriseDevelopment.Contracts.DTOs.Restaurants;

/// <summary>
/// Данные для обновления ресторана
/// </summary>
/// <param name="Name">Новое название ресторана</param>
/// <param name="Address">Новый адрес ресторана</param>
/// <param name="Rating">Новый рейтинг ресторана</param>
/// <param name="OpeningTime">Новое время открытия ресторана</param>
/// <param name="ClosingTime">Новое время закрытия ресторана</param>
public record UpdateRestaurantDto(
    string Name,
    string Address,
    double Rating,
    TimeOnly OpeningTime,
    TimeOnly ClosingTime);
