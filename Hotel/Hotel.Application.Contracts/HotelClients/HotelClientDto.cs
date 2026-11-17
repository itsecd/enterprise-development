namespace Hotel.Application.Contracts.HotelClients;

/// <summary>
/// DTO для получения данных клиента
/// </summary>
/// <param name="Id">Идентификатор клиента</param>
/// <param name="PassportNumber">Номер паспорта</param>
/// <param name="FirstName">Имя</param>
/// <param name="LastName">Фамилия</param>
/// <param name="MiddleName">Отчество</param>
/// <param name="BirthDate">Дата рождения</param>
/// <param name="Citizenship">Гражданство</param>
public record HotelClientDto(
    int Id,
    string PassportNumber,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly BirthDate,
    string Citizenship
) : IHasId<int>;
