using System.ComponentModel.DataAnnotations;

namespace Vet.Contracts.Dtos;

/// <summary>
/// DTO владельца для POST/PUT запросов
/// </summary>
/// <param name="FullName">ФИО</param>
/// <param name="Address">Адрес</param>
/// <param name="PhoneNumber">Номер телефона</param>
public sealed record OwnerCreateUpdateDto(
    [Required(ErrorMessage = "ФИО владельца обязательно для заполнения")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "ФИО должно содержать от 2 до 150 символов")]
    string FullName,

    [MaxLength(250, ErrorMessage = "Адрес не должен превышать 250 символов")]
    string? Address,

    [Required(ErrorMessage = "Номер телефона обязателен для заполнения")]
    [Phone(ErrorMessage = "Некорректный формат номера телефона")]
    [MaxLength(25, ErrorMessage = "Номер телефона не должен превышать 25 символов")]
    string PhoneNumber);