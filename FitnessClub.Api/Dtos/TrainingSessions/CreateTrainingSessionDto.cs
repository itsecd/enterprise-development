
using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Api.Dtos.TrainingSessions;

/// <summary>
/// Данные для создания и изменения записи на занятие.
/// </summary>
public class CreateTrainingSessionDto
{
    /// <summary>
    /// Идентификатор клиента, записанного на занятие.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int ClientId { get; set; }

    /// <summary>
    /// Идентификатор тренера, проводящего занятие.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int TrainerId { get; set; }

    /// <summary>
    /// Дата и время начала занятия.
    /// </summary>
    public DateTime StartsAt { get; set; }

    /// <summary>
    /// Дата и время окончания занятия; может отсутствовать.
    /// </summary>
    public DateTime? EndsAt { get; set; }

    /// <summary>
    /// Идентификатор зала, в котором проходит занятие.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int GymId { get; set; }

    /// <summary>
    /// Признак пробного занятия.
    /// </summary>
    public bool IsTrial { get; set; }
}
