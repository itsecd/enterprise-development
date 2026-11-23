using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoServiceLab.Domain.Entities;

/// <summary>
/// Автомобиль клиента автосервиса
/// </summary>
[Table("cars")]
public class Car
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Гос. номер автомобиля
    /// </summary>
    [Column("license_plate")]
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Марка автомобиля
    /// </summary>
    [Column("brand")]
    public required string Brand { get; set; }

    /// <summary>
    /// Модель автомобиля
    /// </summary>
    [Column("model")]
    public required string Model { get; set; }

    /// <summary>
    /// Год выпуска автомобиля
    /// </summary>
    [Column("year")]
    public int Year { get; set; }

    /// <summary>
    /// Идентификатор клиента автосервиса
    /// </summary>
    [Column("client_id")]
    public int ClientId { get; set; }

    /// <summary>
    /// Клиент автосервиса
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Заказы на работы с автомобилем
    /// </summary>
    public List<ServiceOrder> Orders { get; set; } = [];

    /// <summary>
    /// Возвращает строковое представление автомобиля
    /// </summary>
    public override string ToString() => $"{Brand} {Model} ({Year})";
}
