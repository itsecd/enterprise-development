using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalon.Domain.Entities;

/// <summary>
/// Клиент
/// </summary>
[Table("clients")]
public class Client : Person
{

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    [Column("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    [Column("birthday")]
    public required DateOnly Birthday {  get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    [Column("phone_number")]
    public required string PhoneNumber { get; set; }
}
