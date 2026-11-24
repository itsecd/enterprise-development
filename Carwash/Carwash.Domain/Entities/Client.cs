using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carwash.Domain.Entities;

/// <summary>
/// Класс клиента
/// </summary>
[Table("Clients")]
public class Client
{
    /// <summary>
    /// Уникальный идентификатор клиента
    /// </summary>
    [Key]
    [Column("Id")]
    public required int Id { get; set; }

    /// <summary>
    /// Полное имя клиента
    /// </summary>
    [Column("FullName")]
    public required string FullName { get; set; }

    /// <summary>
    /// Номер телефона клиента
    /// </summary>
    [Column("PhoneNumber")]
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Список автомобилей клиента
    /// </summary>
    public required List<Car> Cars { get; set; } = [];
}
