using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Domain.Entities;

/// <summary>
/// Запись клиента на персональное занятие с тренером.
/// </summary>
public class Training : IEntity
{
    /// <summary>
    /// ID записи.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Клиент, записанный на занятие.
    /// </summary>
    public Client Client { get; set; } = null!;

    /// <summary>Идентификатор связанной сущности Client.</summary>
    public int ClientId { get; set; }

    /// <summary>
    /// Тренер, проводящий занятие.
    /// </summary>
    public Trainer Trainer { get; set; } = null!;

    /// <summary>Идентификатор связанной сущности Trainer.</summary>
    public int TrainerId { get; set; }

    /// <summary>
    /// Зал, в котором проводится занятие.
    /// </summary>
    public Hall Hall { get; set; } = null!;

    /// <summary>Идентификатор связанной сущности Hall.</summary>
    public int HallId { get; set; }

    /// <summary>
    /// Дата и время начала занятия.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Продолжительность занятия в минутах.
    /// </summary>
    public int DurationMinutes { get; set; }

    /// <summary>
    /// Признак пробного занятия.
    /// </summary>
    public bool IsTrial { get; set; }
}