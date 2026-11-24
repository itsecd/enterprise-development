using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carwash.Domain.Entities;

/// <summary>
/// Класс заказа
/// </summary>
[Table("Orders")]
public class Order
{
    /// <summary>
    /// Уникальный идентификатор заказа
    /// </summary>
    [Key]
    [Column("Id")]
    public required int Id { get; set; }

    /// <summary>
    /// Идентификатор клиента, сделавшего заказ
    /// </summary>
    [Column("ClientId")]
    public required int ClientId { get; set; }

    /// <summary>
    /// Клиент, сделавший заказ
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Идентификатор автомобиля, для которого офрмляется заказ
    /// </summary>
    [Column("CarId")]
    public required int CarId { get; set; }

    /// <summary>
    /// Автомобиль, для которой оформляется заказ
    /// </summary>
    public required Car Car { get; set; }

    /// <summary>
    /// Идентификатор услуги, для которой оформляется заказ
    /// </summary>
    [Column("ServiceId")]
    public required int ServiceId { get; set; }

    /// <summary>
    /// Услуга, для которой оформляется заказ
    /// </summary>
    public required Service Service { get; set; }

    /// <summary>
    /// Номер бокса, в котором будет выполняться заказ
    /// </summary>
    [Column("BoxNumber")]
    public required int BoxNumber { get; set; }

    /// <summary>
    /// Дата и время начала заказа
    /// </summary>
    [Column("StartTime")]
    public required DateTime StartTime { get; set; }

    /// <summary>
    /// Дата и время окончания заказа
    /// </summary>
    public DateTime EndTime => StartTime.AddMinutes(Service.DurationInMinutes);
}
