namespace Hotel.Application.Contracts.HotelClients;

/// <summary>
/// DTO для получения данных клиента
/// </summary>
public record HotelClientDto(
    int Id,
    string PassportNumber,
    string FirstName,
    string LastName,
    string? MiddleName,
    DateOnly BirthDate,
    string Citizenship
) : IHasId<int>;
