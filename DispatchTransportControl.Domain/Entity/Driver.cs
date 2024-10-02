using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DispatchTransportControl.Domain.Entity;

/// <summary>
///     Водитель
/// </summary>
[Table("driver")]
public class Driver
{
    /// <summary>
    ///     Уникальный идентификатор
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    ///     Имя
    /// </summary>
    [Column("name")]
    [MaxLength(50)]
    [Required]
    public required string Name { get; set; }

    /// <summary>
    ///     Фамилия
    /// </summary>
    [Column("surname")]
    [MaxLength(50)]
    [Required]
    public required string Surname { get; set; }

    /// <summary>
    ///     Отчество
    /// </summary>
    [Column("patronymic")]
    [MaxLength(50)]
    [Required]
    public required string Patronymic { get; set; }

    /// <summary>
    ///     Паспорт
    /// </summary>
    [Column("passport")]
    [MaxLength(20)]
    [Required]
    public required string Passport { get; set; }

    /// <summary>
    ///     Водительское удостоверение
    /// </summary>
    [Column("driver_license")]
    [MaxLength(20)]
    [Required]
    public required string DriverLicense { get; set; }

    /// <summary>
    ///     Адрес
    /// </summary>
    [Column("address")]
    [MaxLength(256)]
    [Required]
    public required string Address { get; set; }

    /// <summary>
    ///     Телефон
    /// </summary>
    [Column("phone")]
    [MaxLength(20)]
    [Required]
    public required string Phone { get; set; }
}