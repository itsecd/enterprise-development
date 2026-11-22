namespace EnterpriseDevelopment.Contracts.DTOs.Restaurants;

/// <summary>
/// Данные для создания ресторана
/// </summary>
/// <param name="Name">Название создаваемого ресторана</param>
/// <param name="Address">Адрес ресторана</param>
/// <param name="Rating">Рейтинг ресторана</param>
/// <param name="OpeningTime">Время открытия ресторана</param>
/// <param name="ClosingTime">Время закрытия ресторана</param>
public record CreateRestaurantDto(
    string Name,
    string Address,
    double Rating,
    TimeOnly OpeningTime,
    TimeOnly ClosingTime);
