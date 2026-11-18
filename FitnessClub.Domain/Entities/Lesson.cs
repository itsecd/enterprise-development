using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Domain.Entities;

/// <summary>
/// Запись клиента на занятия 
/// </summary>
public class Lesson : IEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор клиента (внешний ключ)
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// Клиент
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Идентификатор тренера (внешний ключ)
    /// </summary>
    public int TrainerId { get; set; }

    /// <summary>
    /// Тренер
    /// </summary>
    public required Trainer Trainer { get; set; }

    /// <summary>
    /// Дата и время начала занятия
    /// </summary>
    public required DateTime DateStart { get; set; }

    /// <summary>
    /// Дата и время окончания занятия
    /// </summary>
    public DateTime? DateEnd { get; set; }

    /// <summary>
    /// Название зала, в котором проходит занятие
    /// </summary>
    public string? Hall { get; set; }

    /// <summary>
    /// Пробное занятие или нет
    /// </summary>
    public bool IsTrial { get; set; }
}
