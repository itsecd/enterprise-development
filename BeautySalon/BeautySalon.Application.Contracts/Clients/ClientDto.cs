using BeautySalon.Shared.Enum;

namespace BeautySalon.Application.Contracts.Clients;

/// <summary>
/// Dto для get запросов к клиентам
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="FullName">Полное имя</param>
/// <param name="Gender">Пол</param>
/// <param name="Birthday">Год рождения</param>
/// <param name="PhoneNumber">Номер телефона</param>
public record ClientDto(int Id, string FullName, Gender Gender, DateOnly Birthday, string PhoneNumber);
