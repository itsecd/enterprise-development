using BeautySalon.Shared.Enum;

namespace BeautySalon.Application.Contracts.Masters;

/// <summary>
/// Dto для get запросов к мастеру
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="FullName">Полное имя</param>
/// <param name="Gender">Пол</param>
/// <param name="PassportNumber">Номер паспорта</param>
/// <param name="Specialization">Специализация</param>
/// <param name="WorkExperience">Стаж работы</param>
public record MasterDto(int Id, string FullName, Gender Gender, string PassportNumber, string Specialization, int WorkExperience);
