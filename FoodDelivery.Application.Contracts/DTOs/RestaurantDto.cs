namespace FoodDelivery.Application.Contracts.DTOs;

/// <summary>
/// DTO ресторана
/// </summary>
/// <param name="Id">Уникальный идентификатор ресторана</param>
/// <param name="Name">Название ресторана</param>
/// <param name="Address">Адрес ресторана</param>
/// <param name="Rating">Рейтинг ресторана</param>
/// <param name="OpeningTime">Время открытия ресторана</param>
/// <param name="ClosingTime">Время закрытия ресторана</param>
public record RestaurantDto(
    int Id,
    string Name,
    string Address,
    double Rating,
    TimeOnly OpeningTime,
    TimeOnly ClosingTime
);
