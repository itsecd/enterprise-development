namespace FitnesClub.Domain.Entities;

/// <summary>
/// Запись клиента на персональное занятие с тренером
/// </summary>
public class Booking
{
    /// <summary>
    /// Идентификатор записи
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required Guid MemberId { get; set; }

    /// <summary>
    /// Клиент, записанный на занятие
    /// </summary>
    public Member? Member { get; set; }

    /// <summary>
    /// Идентификатор тренера
    /// </summary>
    public required Guid TrainerId { get; set; }

    /// <summary>
    /// Тренер, проводящий занятие
    /// </summary>
    public Trainer? Trainer { get; set; }

    /// <summary>
    /// Дата и время занятия
    /// </summary>
    public required DateTime LessonDateTime { get; set; }

    /// <summary>
    /// Длительность занятия
    /// </summary>
    public required TimeSpan Duration { get; set; }

    /// <summary>
    /// Название зала
    /// </summary>
    /// <example>Зал №1</example>
    public required string RoomName { get; set; }

    /// <summary>
    /// Признак пробного посещения
    /// </summary>
    public bool IsTrial { get; set; }
}