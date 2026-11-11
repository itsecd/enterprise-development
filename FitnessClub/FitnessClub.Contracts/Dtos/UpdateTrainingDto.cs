using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Dtos;

/// <summary>
/// Данные для обновления записи на тренировку.
/// </summary>
public class UpdateTrainingDto
{
    /// <summary>ID клиента.</summary>
    [Range(1, int.MaxValue)]
    public int ClientId { get; set; }

    /// <summary>ID тренера.</summary>
    [Range(1, int.MaxValue)]
    public int TrainerId { get; set; }

    /// <summary>ID зала.</summary>
    [Range(1, int.MaxValue)]
    public int HallId { get; set; }

    /// <summary>Дата и время начала тренировки.</summary>
    public DateTime StartTime { get; set; }

    /// <summary>Продолжительность тренировки в минутах.</summary>
    [Range(1, 480)]
    public int DurationMinutes { get; set; }

    /// <summary>Признак пробной тренировки.</summary>
    public bool IsTrial { get; set; }
}