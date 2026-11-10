using BeautySalon.Contracts.Interfaces;
using BeautySalon.Shared.Enums;

namespace BeautySalon.Contracts.Dto;

/// <summary>
/// Мастер салона (для чтения)
/// </summary>
/// <param name="Id">Уникальный идентификатор</param>
/// <param name="FullName">Полное имя</param>
/// <param name="Gender">Пол</param>
/// <param name="PassportNumber">Номер паспорта</param>
/// <param name="Specialization">Специализация</param>
/// <param name="WorkExperience">Стаж работы (лет)</param>
public record MasterDto(
    int Id,
    string FullName,
    Gender Gender,
    string PassportNumber,
    Specialization Specialization,
    int WorkExperience) : IHasId;