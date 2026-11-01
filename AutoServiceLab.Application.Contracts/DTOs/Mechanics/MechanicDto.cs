using AutoServiceLab.Domain.Enums;

namespace AutoServiceLab.Application.Contracts.DTOs.Mechanics;

/// <summary>
/// DTO для механика
/// </summary>
/// <param name="Id">Идентификатор механика</param>
/// <param name="FullName">ФИО механика</param>
/// <param name="Specialization">Специализация</param>
/// <param name="ExperienceYears">Стаж работы (лет)</param>
public record MechanicDto(
    int Id,
    string FullName,
    MechanicSpecialization Specialization,
    int ExperienceYears
);
