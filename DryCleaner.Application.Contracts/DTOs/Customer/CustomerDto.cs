namespace DryCleaner.Application.Contracts.DTOs.Customer;

/// <summary>
/// DTO клиента
/// </summary>
/// <param name="Id">Идентификатор клиента</param>
/// <param name="LastName">Фамилия клиента</param>
/// <param name="FirstName">Имя клиента</param>
/// <param name="Patronymic">Отчество клиента</param>
/// <param name="PhoneNumber">Номер телефона клиента</param>
public record CustomerDto(
    int Id,
    string FirstName,
    string? Patronymic,
    string LastName,
    string PhoneNumber
);