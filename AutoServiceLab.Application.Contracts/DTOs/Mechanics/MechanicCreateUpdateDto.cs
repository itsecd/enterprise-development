using AutoServiceLab.Domain.Enums;

namespace AutoServiceLab.Application.Contracts.DTOs.Mechanics;

/// <summary>
/// DTO для создания или обновления механика
/// </summary>
/// <param name="FullName">ФИО механика</param>
/// <param name="PassportNumber">Номер паспорта</param>
/// <param name="Phone">Номер телефона</param>
/// <param name="Specialization">Специализация</param>
/// <param name="ExperienceYears">Стаж работы (лет)</param>
public record MechanicCreateUpdateDto(
    string FullName,
    string PassportNumber,
    string Phone,
    MechanicSpecialization Specialization,
    int ExperienceYears
);
