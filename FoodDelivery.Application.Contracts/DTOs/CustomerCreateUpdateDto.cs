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
    [Required(ErrorMessage = "Фамилия клиента обязательна")]
    [StringLength(100, ErrorMessage = "Фамилия не должна превышать 100 символов")]
    [RegularExpression(@".*\S.*", ErrorMessage = "Фамилия не должна состоять только из пробелов")]
    string LastName,

    [Required(ErrorMessage = "Имя клиента обязательно")]
    [StringLength(100, ErrorMessage = "Имя не должно превышать 100 символов")]
    [RegularExpression(@".*\S.*", ErrorMessage = "Имя не должно состоять только из пробелов")]
    string FirstName,

    [StringLength(100, ErrorMessage = "Отчество не должно превышать 100 символов")]
    [RegularExpression(@"^$|.*\S.*", ErrorMessage = "Отчество не должно состоять только из пробелов")]
    string? Patronymic,

    [Required(ErrorMessage = "Номер телефона обязателен")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "Номер телефона должен содержать от 5 до 20 символов")]
    [RegularExpression(@"^\+?[0-9()\-\s]{5,20}$", ErrorMessage = "Укажите корректный номер телефона")]
    string Phone,

    [Required(ErrorMessage = "Адрес доставки обязателен")]
    [StringLength(250, ErrorMessage = "Адрес доставки не должен превышать 250 символов")]
    [RegularExpression(@".*\S.*", ErrorMessage = "Адрес доставки не должен состоять только из пробелов")]
    string DeliveryAddress
);