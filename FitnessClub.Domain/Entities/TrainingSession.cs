namespace FitnessClub.Domain.Entities;

/// <summary>
/// Запись клиента на персональное занятие.
/// </summary>
public class TrainingSession
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Клиент, записанный на занятие.
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Тренер, проводящий занятие.
    /// </summary>
    public required Trainer Trainer { get; set; }

    /// <summary>
    /// Дата и время начала занятия.
    /// </summary>
    public required DateTime StartsAt { get; set; }

    /// <summary>
    /// Дата и время окончания занятия.
    /// </summary>
    public DateTime? EndsAt { get; set; }

    /// <summary>
    /// Зал, в котором проходит занятие.
    /// </summary>
    public required Gym Gym { get; set; }

    /// <summary>
    /// Является ли занятие пробным.
    /// </summary>
    public bool IsTrial { get; set; }
}