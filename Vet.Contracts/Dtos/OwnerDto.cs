namespace Vet.Contracts.Dtos;

/// <summary>
/// DTO владельца для GET запросов
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="FullName">ФИО</param>
/// <param name="Address">Адрес</param>
/// <param name="PhoneNumber">Номер телефона</param>
public sealed record OwnerDto(int Id, string FullName, string? Address, string PhoneNumber);
