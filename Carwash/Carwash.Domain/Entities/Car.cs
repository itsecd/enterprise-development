using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carwash.Domain.Entities;

/// <summary>
/// Класс автомобиля
/// </summary>
[Table("cars")]
public class Car
{
    /// <summary>
    /// Уникальный идентификатор автомобиля
    /// </summary>
    [Key]
    [Column("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Государственный номер автомобиля
    /// </summary>
    [Column("license_plate")]
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Марка автомобиля
    /// </summary>
    [Column("brand")]
    public required string Brand { get; set; }

    /// <summary>
    /// Идентификатор клиента, которому принадлежит автомобиль
    /// </summary>
    [Column("client_id")]
    public required int ClientId { get; set; }

    /// <summary>
    /// Клиент, которому принадлежит автомобиль
    /// </summary>
    public required Client Client { get; set; }
}
