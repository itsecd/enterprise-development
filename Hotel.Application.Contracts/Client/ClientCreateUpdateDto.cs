namespace Hotel.Application.Contracts.Client;
/// <summary>
/// DTO для PUT/POST запросов к клиентам
/// </summary>
/// <param name="Name">Имя клиента</param>
/// <param name="Surname">Фамилия клиента</param>
/// <param name="Patronymic">отчество клиента</param>
/// <param name="PassportNumber">Номер паспорта клиента</param>
/// <param name="BirthDate">Дата рождения клиента</param>
/// <param name="Citizenship">Гражданство клиента</param>
public record ClientCreateUpdateDto(
    string? Name,
    string? Surname,
    string? Patronymic,
    string? PassportNumber,
    DateOnly? BirthDate,
    string? Citizenship
);