using System.ComponentModel.DataAnnotations;

namespace Hotel.Application.Contracts.HotelClients;

/// <summary>
/// DTO для создания клиента
/// </summary>
public record CreateHotelClientDto(
    [Required(ErrorMessage = "Номер паспорта обязателен")]
    [StringLength(8, MinimumLength = 8, ErrorMessage = "Номер паспорта содержит ровно 8 символов")]
    string PassportNumber,

    [Required(ErrorMessage = "Имя обязательно")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Имя должно содержать от 1 до 100 символов")]
    string FirstName,

    [Required(ErrorMessage = "Фамилия обязательна")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Фамилия должна содержать от 1 до 100 символов")]
    string LastName,

    [StringLength(100, ErrorMessage = "Отчество не должно превышать 100 символов")]
    string? MiddleName,

    DateOnly BirthDate,

    [Required(ErrorMessage = "Гражданство обязательно")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Гражданство должно содержать от 3 до 100 символов")]
    string Citizenship
);
