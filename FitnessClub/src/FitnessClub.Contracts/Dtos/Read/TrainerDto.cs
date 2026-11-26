using FitnessClub.Contracts.Interfaces;
using FitnessClub.Shared.Enums;

namespace FitnessClub.Contracts.Dtos.Read;
/// <summary>
/// Данные тренера для ответа API
/// </summary>
/// <param name="Id">Идентификатор тренера</param>
/// <param name="FullName">Полное имя</param>
/// <param name="PassportNumber">Номер паспорта</param>
/// <param name="Gender">Пол</param>
/// <param name="BirthDate">Дата рождения</param>
/// <param name="PhoneNumber">Номер телефона</param>
/// <param name="SpecializationId">Идентификатор специализации</param>
/// <param name="SpecializationName">Название специализации</param>
/// <param name="Experience">Опыт работы в годиках</param>
public record TrainerDto
(
    int Id,
    string FullName,
    string PassportNumber,
    Gender Gender,
    DateOnly BirthDate,
    string? PhoneNumber,
    int SpecializationId,
    string SpecializationName,
    int Experience
) : IHasId;