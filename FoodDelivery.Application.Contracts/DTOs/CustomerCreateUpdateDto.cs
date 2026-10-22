using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Application.Contracts.DTOs;

/// <summary>
/// DTO для создания или обновления клиента
/// </summary>
/// <param name="LastName">Фамилия клиента</param>
/// <param name="FirstName">Имя клиента</param>
/// <param name="Patronymic">Отчество клиента, если оно указано</param>
/// <param name="Phone">Номер телефона клиента</param>
/// <param name="DeliveryAddress">Адрес доставки клиента</param>
public record CustomerCreateUpdateDto(
    [property: Required(ErrorMessage = "Фамилия клиента обязательна")]
    [property: StringLength(100, ErrorMessage = "Фамилия не должна превышать 100 символов")]
    [property: RegularExpression(@".*\S.*", ErrorMessage = "Фамилия не должна состоять только из пробелов")]
    string LastName,

    [property: Required(ErrorMessage = "Имя клиента обязательно")]
    [property: StringLength(100, ErrorMessage = "Имя не должно превышать 100 символов")]
    [property: RegularExpression(@".*\S.*", ErrorMessage = "Имя не должно состоять только из пробелов")]
    string FirstName,

    [property: StringLength(100, ErrorMessage = "Отчество не должно превышать 100 символов")]
    [property: RegularExpression(@"^$|.*\S.*", ErrorMessage = "Отчество не должно состоять только из пробелов")]
    string? Patronymic,

    [property: Required(ErrorMessage = "Номер телефона обязателен")]
    [property: StringLength(20, MinimumLength = 5, ErrorMessage = "Номер телефона должен содержать от 5 до 20 символов")]
    [property: RegularExpression(@"^\+?[0-9()\-\s]{5,20}$", ErrorMessage = "Укажите корректный номер телефона")]
    string Phone,

    [property: Required(ErrorMessage = "Адрес доставки обязателен")]
    [property: StringLength(250, ErrorMessage = "Адрес доставки не должен превышать 250 символов")]
    [property: RegularExpression(@".*\S.*", ErrorMessage = "Адрес доставки не должен состоять только из пробелов")]
    string DeliveryAddress
);