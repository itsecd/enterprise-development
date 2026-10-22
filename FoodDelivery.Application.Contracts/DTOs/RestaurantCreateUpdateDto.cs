using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Application.Contracts.DTOs;

/// <summary>
/// DTO для создания или обновления ресторана
/// </summary>
/// <param name="Name">Название ресторана</param>
/// <param name="Address">Адрес ресторана</param>
/// <param name="Rating">Рейтинг ресторана</param>
/// <param name="OpeningTime">Время открытия ресторана</param>
/// <param name="ClosingTime">Время закрытия ресторана</param>
public record RestaurantCreateUpdateDto(
    [property: Required(ErrorMessage = "Название ресторана обязательно")]
    [property: StringLength(100, ErrorMessage = "Название ресторана не должно превышать 100 символов")]
    [property: RegularExpression(@".*\S.*", ErrorMessage = "Название ресторана не должно состоять только из пробелов")]
    string Name,

    [property: Required(ErrorMessage = "Адрес ресторана обязателен")]
    [property: StringLength(250, ErrorMessage = "Адрес ресторана не должен превышать 250 символов")]
    [property: RegularExpression(@".*\S.*", ErrorMessage = "Адрес ресторана не должен состоять только из пробелов")]
    string Address,

    [property: Range(0, 5, ErrorMessage = "Рейтинг ресторана должен быть от 0 до 5")]
    double Rating,

    TimeOnly OpeningTime,

    TimeOnly ClosingTime
);