using FitnesClub.Domain.Enums;

namespace FitnesClub.Api.Contracts.Trainers;

/// <summary>
/// Информация о тренере фитнес-клуба.
/// </summary>
/// <param name="Id">Уникальный идентификатор тренера.</param>
/// <param name="PassportNumber">Серия и номер паспорта тренера.</param>
/// <param name="FirstName">Имя тренера.</param>
/// <param name="LastName">Фамилия тренера.</param>
/// <param name="PhoneNumber">Контактный номер телефона.</param>
/// <param name="Email">Адрес электронной почты (опционально).</param>
/// <param name="Gender">Пол тренера.</param>
/// <param name="BirthDate">Дата рождения тренера.</param>
/// <param name="SpecializationId">Идентификатор основной специализации тренера.</param>
/// <param name="WorkExperienceYears">Стаж профессиональной тренерской работы (в годах).</param>
public record TrainerDto(
    Guid Id,
    string PassportNumber,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string? Email,
    Gender Gender,
    DateOnly BirthDate,
    Guid SpecializationId,
    int WorkExperienceYears
);