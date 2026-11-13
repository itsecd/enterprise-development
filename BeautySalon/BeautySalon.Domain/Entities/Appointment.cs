using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalon.Domain.Entities;

/// <summary>
/// Запись
/// </summary>
[Table("appointments")]
public class Appointment
{

    /// <summary>
    /// Идентификатор записи
    /// </summary>
    [Column("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Клиент
    /// </summary>
    [Column("client")]
    public required Client Client { get; set; }

    /// <summary>
    /// Услуга
    /// </summary>
    [Column("service")]
    public required Service Service { get; set; }

    /// <summary>
    /// Мастер
    /// </summary>
    [Column("master")]
    public required Master Master { get; set; }

    /// <summary>
    /// Дата и время
    /// </summary>
    [Column("start_time")]
    public required DateTime StartTime { get; set; }

    /// <summary>
    /// Постоянный клиент
    /// </summary>
    [Column("is_regular")]
    public required bool IsRegular { get; set; }
}
