using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DispatchTransportControl.Domain;

/// <summary>
/// Транспортное средство
/// </summary>
[Table("vehicle")]
public class Vehicle
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Гос. номер транспортного средства
    /// </summary>
    [Column("registration_number")]
    [MaxLength(20)]
    [Required]
    public required string RegistrationNumber { get; set; }

    /// <summary>
    /// Тип транспортного средства
    /// </summary>
    [Column("vehicle_type")]
    [Required]
    public required VehicleType VehicleType { get; set; }

    /// <summary>
    /// Модель транспортного средства
    /// </summary>
    [Column("vehicle_model")]
    [Required]
    public required VehicleModel VehicleModel { get; set; }

    /// <summary>
    /// Год выпуска транспортного средства
    /// </summary>
    [Column("year_of_manufacture")]
    [Required]
    public required int YearOfManufacture { get; set; }
}

/// <summary>
/// Типы транспортных средств
/// </summary>
public enum VehicleType
{
    Bus,
    Trolley,
    Tram
}