using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DispatchTransportControl.Domain.Entity;

/// <summary>
///     Модель транспортного средства
/// </summary>
[Table("vehicle_model")]
public class VehicleModel
{
    /// <summary>
    ///     Уникальный идентификатор
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    ///     Название модели
    /// </summary>
    [Column("name")]
    [MaxLength(50)]
    [Required]
    public required string Name { get; set; }

    /// <summary>
    ///     Низкопольная модель или нет
    /// </summary>
    [Column("low_floor")]
    [Required]
    public required bool LowFloor { get; set; }

    /// <summary>
    ///     Максимальная вместимость
    /// </summary>
    [Column("max_capacity")]
    [Required]
    public required int MaxCapacity { get; set; }
}