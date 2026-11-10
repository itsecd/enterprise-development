using BeautySalon.Shared.Enum;

namespace BeautySalon.Application.Contracts.Masters;

/// <summary>
/// Dto для put и post запросов к мастеру
/// </summary>
/// <param name="FullName">Полное имя</param>
/// <param name="Gender">Пол</param>
/// <param name="PassportNumber">Номер паспорта</param>
/// <param name="Specialization">Специализация</param>
/// <param name="WorkExperience">Стаж работы</param>
public record MasterCreateUpdateDto(string FullName, Gender Gender, string PassportNumber, string Specialization, int WorkExperience);
