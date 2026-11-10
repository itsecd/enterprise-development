using System.ComponentModel.DataAnnotations;

namespace Vet.Contracts.Dtos;

/// <summary>
/// DTO владельца для POST/PUT запросов
/// </summary>
/// <param name="FullName">ФИО</param>
/// <param name="Address">Адрес</param>
/// <param name="PhoneNumber">Номер телефона</param>
public sealed record OwnerCreateUpdateDto(
    [property: Required(ErrorMessage = "ФИО владельца обязательно для заполнения")]
    [property: StringLength(150, MinimumLength = 2, ErrorMessage = "ФИО должно содержать от 2 до 150 символов")]
    string FullName,

    [property: MaxLength(250, ErrorMessage = "Адрес не должен превышать 250 символов")]
    string? Address,

    [property: Required(ErrorMessage = "Номер телефона обязателен для заполнения")]
    [property: Phone(ErrorMessage = "Некорректный формат номера телефона")]
    [property: MaxLength(25, ErrorMessage = "Номер телефона не должен превышать 25 символов")]
    string PhoneNumber);