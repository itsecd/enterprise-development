namespace FitnessGym.Contracts.Dto;

/// <summary>
/// DTO записи клиента на персональное занятие.
/// </summary>
public class TrainingSessionDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    public int ClientId { get; init; }

    /// <summary>
    /// Идентификатор тренера.
    /// </summary>
    public int TrainerId { get; init; }

    /// <summary>
    /// Дата и время начала занятия.
    /// </summary>
    public DateTime StartsAt { get; init; }

    /// <summary>
    /// Дата и время окончания занятия; может быть не проставлена.
    /// </summary>
    public DateTime? EndsAt { get; init; }

    /// <summary>
    /// Название зала, в котором проходит занятие.
    /// </summary>
    public string HallName { get; init; } = string.Empty;

    /// <summary>
    /// Является ли посещение пробным.
    /// </summary>
    public bool IsTrial { get; init; }
}
