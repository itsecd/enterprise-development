using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Domain.Entities;

/// <summary>
/// Ресторан-партнер службы доставки
/// </summary>
[Table("Restaurants")]
public class Restaurant
{
    /// <summary>
    /// Уникальный идентификатор ресторана
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Название заведения
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Адрес ресторана
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Телефон ресторана
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Флаг активности
    /// </summary>
    public bool IsActive { get; set; } = true;
}