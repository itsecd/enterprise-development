using System.ComponentModel.DataAnnotations;

namespace CarWash.Application.Contracts.Customers;

/// <summary>
/// DTO для POST/PUT запросов к клиентам
/// </summary>
public record CarWashCustomerCreateUpdateDto(
    [param: Required(ErrorMessage = "Имя клиента обязательно")]
    [param: StringLength(100, ErrorMessage = "Имя не должна превышать 100 символов")]
    string LastName,

    [param: Required(ErrorMessage = "Фамилия клиента обязательна")]
    [param: StringLength(100, ErrorMessage = "Фамилия не должно превышать 100 символов")]
    string FirstName,

    string? Patronymic,

    [param: Required(ErrorMessage = "Номер телефона обязателен")]
    [param: RegularExpression(@"^(\+7|8)?\d{10}$|^\d{7}$", ErrorMessage = "Укажите корректный номер телефона, например +71234567890 или 4567890")]
    string Phone);