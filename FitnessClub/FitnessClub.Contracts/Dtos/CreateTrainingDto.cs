using FitnessClub.Contracts.Validation;
using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Dtos;

/// <summary>
/// Данные для создания записи на тренировку.
/// </summary>
public class CreateTrainingDto
{
    /// <summary>
    /// ID клиента.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int ClientId { get; set; }

    /// <summary>
    /// ID тренера.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int TrainerId { get; set; }

    /// <summary>
    /// ID зала.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int HallId { get; set; }

    /// <summary>
    /// Дата и время начала тренировки.
    /// </summary>
    [ValidDate]
    public DateTimeOffset StartTime { get; set; }

    /// <summary>
    /// Продолжительность тренировки в минутах.
    /// </summary>
    [Range(1, 480)]
    public int DurationMinutes { get; set; }

    /// <summary>
    /// Признак пробной тренировки.
    /// </summary>
    public bool IsTrial { get; set; }
}