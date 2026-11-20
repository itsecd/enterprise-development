using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Domain.Entities;

/// <summary>
/// Курьер службы доставки
/// </summary>
[Table("Couriers")]
public class Courier
{
    /// <summary>
    /// Уникальный идентификатор курьера
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// ФИО курьера
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Контактный номер телефона курьера
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Транспортное средство курьера (необязательное поле)
    /// </summary>
    /// <example>Велосипед</example>
    public string? Vehicle { get; set; }

    /// <summary>
    /// Находится ли курьер на смене
    /// </summary>
    public bool IsActive { get; set; } = true;
}