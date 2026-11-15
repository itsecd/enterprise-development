using FitnessGym.Domain.Enums;

namespace FitnessGym.Domain.Entities;

/// <summary>
/// Запись клиента на персональное занятие к тренеру
/// </summary>
public sealed class TrainingSession
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Клиент, записавшийся на занятие
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Прикреплённый тренер
    /// </summary>
    public required Trainer Trainer { get; set; }

    /// <summary>
    /// Дата и время начала занятия
    /// </summary>
    public required DateTime StartsTraining { get; set; }

    /// <summary>
    /// Дата и время окончания занятия
    /// </summary>
    public required DateTime EndsTraining { get; set; }

    /// <summary>
    /// Зал для занятия
    /// </summary>
    public required Hall HallName { get; set; }

    /// <summary>
    /// Пробное ли посещение
    /// </summary>
    public required bool IsTrial { get; set; }

    /// <summary>
    /// Определяет, идёт ли занятие в указанный момент времени
    /// </summary>
    public bool IsInProgressAt(DateTime moment) =>
        StartsTraining <= moment && moment < EndsTraining;
}
