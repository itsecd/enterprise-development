using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DispatchTransportControl.Domain.Entity;

/// <summary>
///     Назначение водителя и транспортного средства на маршрут
/// </summary>
[Table("route_assignment")]
public class RouteAssignment
{
    /// <summary>
    ///     Уникальный идентификатор
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    ///     Транспортное средство
    /// </summary>
    [Column("vehicle")]
    [Required]
    public required Vehicle Vehicle { get; set; }

    /// <summary>
    ///     Водитель
    /// </summary>
    [Column("driver")]
    [Required]
    public required Driver Driver { get; set; }

    /// <summary>
    ///     Номер маршрута
    /// </summary>
    [Column("route_number")]
    [MaxLength(20)]
    [Required]
    public required string RouteNumber { get; set; }

    /// <summary>
    ///     Дата и время выхода на рейс
    /// </summary>
    [Column("start_time")]
    [Required]
    public required DateTime StartTime { get; set; }

    /// <summary>
    ///     Дата и время окончания рейса
    /// </summary>
    [Column("end_time")]
    [Required]
    public required DateTime EndTime { get; set; }
}