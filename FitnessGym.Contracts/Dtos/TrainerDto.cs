namespace FitnessGym.Contracts.Dtos;

/// <summary>
/// DTO для представления информации о тренере фитнес-клуба
/// </summary>
/// <param name="Id">Уникальный идентификатор тренера</param>
/// <param name="PassportNumber">Номер паспорта тренера</param>
/// <param name="FullName">ФИО тренера</param>
/// <param name="Gender">Пол тренера (Male/Female)</param>
/// <param name="BirthDate">Дата рождения тренера</param>
/// <param name="SpecializationId">Идентификатор специализации</param>
/// <param name="SpecializationName">Название специализации</param>
/// <param name="ExperienceYears">Стаж работы в годах</param>
public record TrainerDto(
    int Id,
    string PassportNumber,
    string FullName,
    string Gender,
    DateOnly BirthDate,
    int SpecializationId,
    string SpecializationName,
    int ExperienceYears);