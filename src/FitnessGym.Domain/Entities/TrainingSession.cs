using FitnessGym.Domain.Interfaces;
namespace FitnessGym.Domain.Entities;

/// <summary>
/// Запись клиента на персональное занятие к тренеру.
/// </summary>
public class TrainingSession : IIdentified
{
    /// <summary>
    /// Длительность занятия для проверки занятости зала, если время окончания ещё не указано.
    /// </summary>
    public const int DefaultDurationMinutes = 60;

    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Клиент, записанный на занятие.
    /// </summary>
    public Client Client { get; set; } = null!;

    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// Тренер, проводящий занятие.
    /// </summary>
    public Trainer Trainer { get; set; } = null!;

    /// <summary>
    /// Идентификатор тренера.
    /// </summary>
    public int TrainerId { get; set; }

    /// <summary>
    /// Дата и время начала занятия.
    /// </summary>
    public required DateTime StartsAt { get; set; }

    /// <summary>
    /// Дата и время окончания занятия.
    /// В момент записи клиента точное время окончания может быть неизвестно,
    /// поэтому значение может не быть проставлено. В таком случае зал считается занятым
    /// в течение <see cref="DefaultDurationMinutes"/> минут с начала занятия.
    /// </summary>
    public DateTime? EndsAt { get; set; }

    /// <summary>
    /// Название зала, в котором проходит занятие.
    /// </summary>
    public required string HallName { get; set; }

    /// <summary>
    /// Является ли посещение пробным.
    /// </summary>
    public required bool IsTrial { get; set; }
}
