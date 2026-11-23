namespace Hotel.Application.Contracts.Client;
/// <summary>
/// DTO для GET запросов к клиентам
/// </summary>
/// <param name="Id">Id клиента</param>
/// <param name="Name">Имя клиента</param>
/// <param name="Surname">Фамилия клиента</param>
/// <param name="Patronymic">отчество клиента</param>
/// <param name="PassportNumber">Номер паспорта клиента</param>
/// <param name="BirthDate">Дата рождения клиента</param>
/// <param name="Citizenship">Гражданство клиента</param>
public record ClientResponseDto(
    int Id,
    string Name,
    string Surname,
    string? Patronymic,
    string PassportNumber,
    DateOnly BirthDate,
    string Citizenship
);