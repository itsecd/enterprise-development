using System.ComponentModel.DataAnnotations;

namespace FitnessGym.Contracts.Dtos;

/// <summary>
/// DTO для создания записи на занятие
/// </summary>
/// <param name="ClientId">Идентификатор клиента (обязательное поле)</param>
/// <param name="TrainerId">Идентификатор тренера (обязательное поле)</param>
/// <param name="StartsTraining">Дата и время начала занятия (обязательное поле)</param>
/// <param name="EndsTraining">Дата и время окончания занятия (обязательное поле)</param>
/// <param name="Hall">Название зала (обязательное поле)</param>
/// <param name="IsTrial">Является ли посещение пробным</param>

public record TrainingSessionEditDto(
    [Required]
    int ClientId,

    [Required]
    int TrainerId,

    [Required]
    DateTime StartsTraining,

    [Required]
    DateTime EndsTraining,

    [Required]
    string Hall,

    bool IsTrial);