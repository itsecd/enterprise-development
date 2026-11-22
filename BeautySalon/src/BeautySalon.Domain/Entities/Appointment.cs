using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Domain.Entities;

/// <summary>
/// Запись клиента на приём к мастеру
/// </summary>
public class Appointment : IEntity
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Мастер
    /// </summary>
    public required Master Master { get; set; }

    /// <summary>
    /// Клиент
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Услуга
    /// </summary>
    public required Service Service { get; set; }

    /// <summary>
    /// Дата и время приёма
    /// </summary>
    public DateTime DateTime { get; set; }

    /// <summary>
    /// Признак постоянного клиента
    /// </summary>
    public bool IsRegularClient { get; set; }
}