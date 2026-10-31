using System.ComponentModel.DataAnnotations;

namespace CarWash.Application.Contracts.Customers;

/// <summary>
/// DTO для POST/PUT запросов к клиентам
/// </summary>
public record CarWashCustomerCreateUpdateDto(
    [property: Required(ErrorMessage = "Имя клиента обязательно")]
    [property: StringLength(100, ErrorMessage = "Фамилия не должна превышать 100 символов")]
    string LastName,

    [property: Required(ErrorMessage = "Фамилия клиента обязательна")]
    [property: StringLength(100, ErrorMessage = "Имя не должно превышать 100 символов")]
    string FirstName,

    string? Patronymic,

    [property: Required(ErrorMessage = "Номер телефона обязателен")]
    [property: RegularExpression(@"^(\+7|8)?\d{10}$|^\d{7}$", ErrorMessage = "Укажите корректный номер телефона, например +71234567890 или 4567890")]
    string Phone);