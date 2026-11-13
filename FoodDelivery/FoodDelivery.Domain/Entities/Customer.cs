using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Domain.Entities;

/// <summary>
/// Клиент службы доставки еды
/// </summary>
[Table("Customers")]
public class Customer
{
    /// <summary>
    /// Уникальный идентификатор клиента
    /// </summary>
    [Key]
    public int Id {  get; set; }

    /// <summary>
    /// ФИО клиента
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Номер телефона клиента
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Адрес электронной почты
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Адрес доставки
    /// </summary>
    public string? DefaultAdress { get; set; }
}