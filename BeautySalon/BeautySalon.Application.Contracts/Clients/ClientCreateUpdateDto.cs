using BeautySalon.Shared.Enum;

namespace BeautySalon.Application.Contracts.Clients;

/// <summary>
/// Dto для put и post запросов к клиентам
/// </summary>
/// <param name="FullName">Полное имя</param>
/// <param name="Genred">Пол</param>
/// <param name="Birthday">Год рождения</param>
/// <param name="PhoneNumber">Номер телефона</param>
public record ClientCreateUpdateDto(string FullName, Gender Gender, DateOnly Birthday, string PhoneNumber);
