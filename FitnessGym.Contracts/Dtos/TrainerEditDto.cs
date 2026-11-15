using System.ComponentModel.DataAnnotations;

namespace FitnessGym.Contracts.Dtos;

/// <summary>
/// DTO для создания или обновления тренера
/// </summary>
/// <param name="PassportNumber">Номер паспорта (обязательное поле, до 20 символов)</param>
/// <param name="FullName">ФИО тренера (обязательное поле, до 200 символов)</param>
/// <param name="Gender">Пол тренера (Male/Female, обязательное поле)</param>
/// <param name="BirthDate">Дата рождения (обязательное поле)</param>
/// <param name="SpecializationId">Идентификатор специализации (обязательное поле)</param>
/// <param name="ExperienceYears">Стаж работы от 0 до 60 лет (обязательное поле)</param>
public record TrainerEditDto(
    [Required, MaxLength(20)]
    string PassportNumber,

    [Required, MaxLength(200)]
    string FullName,

    [Required]
    string Gender,

    [Required]
    DateOnly BirthDate,

    [Required]
    int SpecializationId,

    [Required, Range(0, 60)]
    int ExperienceYears);