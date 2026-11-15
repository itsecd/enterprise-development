using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoServiceLab.Domain.Entities;

/// <summary>
/// Клиент автосервиса
/// </summary>
[Table("clients")]
public class Client
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// ФИО клиента
    /// </summary>
    [Column("full_name")]
    public required string FullName { get; set; }

    /// <summary>
    /// Заказы клиента
    /// </summary>
    public List<ServiceOrder> Orders { get; set; } = [];

    /// <summary>
    /// Номер телефона клиента
    /// </summary>
    [Column("phone")]
    public required string Phone { get; set; }

    /// <summary>
    /// Автомобили клиента
    /// </summary>
    public List<Car> Cars { get; set; } = [];

    /// <summary>
    /// Возвращает строковое представление клиента
    /// </summary>
    public override string ToString() => $"{FullName} ({Phone})";
}
