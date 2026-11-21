namespace Hotel.Contracts.DTO;

/// <summary>
/// Dto для передачи информации о клиенте
/// </summary>
/// <param name="Id">Id клиента</param>
/// <param name="LastName">Фамилия</param>
/// <param name="FirstName">Имя</param>
/// <param name="Patronymic">Отчество</param>
/// <param name="BirthDate">Дата рождения</param>
/// <param name="NumberPassport">Номер паспорта</param>
/// <param name="Citizenship">Гражданство</param>
public sealed record ClientDto(
    int Id,
    string LastName,
    string FirstName,
    string? Patronymic,
    DateTime BirthDate,
    string NumberPassport,
    string Citizenship);