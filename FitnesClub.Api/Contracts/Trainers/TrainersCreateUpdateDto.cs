using FitnesClub.Domain.Enums;

namespace FitnesClub.Api.Contracts.Trainers;

/// <summary>
/// Данные для создания или обновления карточки тренера.
/// </summary>
/// <param name="FirstName">Имя тренера.</param>
/// <param name="LastName">Фамилия тренера.</param>
/// <param name="PassportNumber">Серия и номер паспорта тренера.</param>
/// <param name="PhoneNumber">Контактный номер телефона.</param>
/// <param name="Email">Адрес электронной почты (опционально).</param>
/// <param name="Gender">Пол тренера.</param>
/// <param name="BirthDate">Дата рождения тренера.</param>
/// <param name="SpecializationId">Идентификатор привязанной специализации.</param>
/// <param name="WorkExperienceYears">Стаж тренерской деятельности (в годах).</param>
public record TrainersCreateUpdateDto(
    string FirstName,
    string LastName,
    string PassportNumber,
    string PhoneNumber,
    string? Email,
    Gender Gender,
    DateOnly BirthDate,
    Guid SpecializationId,
    int WorkExperienceYears
);