using BeautySalon.Contracts.Interfaces;
using BeautySalon.Shared.Enums;

namespace BeautySalon.Contracts.Dto;

/// <summary>
/// Клиент салона (для чтения)
/// </summary>
/// <param name="Id">Уникальный идентификатор</param>
/// <param name="FullName">Полное имя</param>
/// <param name="Gender">Пол</param>
/// <param name="BirthDate">Дата рождения</param>
/// <param name="PhoneNumber">Номер телефона</param>
public record ClientDto(
    int Id,
    string FullName,
    Gender Gender,
    DateOnly BirthDate,
    string PhoneNumber) : IHasId;