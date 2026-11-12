using Carwash.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carwash.Domain.Entities;

/// <summary>
/// Класс услуги
/// </summary>
[Table("services")]
public class Service
{
    /// <summary>
    /// Уникальный идентификатор услуги
    /// </summary>
    [Key]
    [Column("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Название услуги
    /// </summary>
    [Column("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Цена услуги
    /// </summary>
    [Column("price")]
    public required decimal Price { get; set; }

    /// <summary>
    /// Категория автомобиля, для которой предназначена услуга
    /// </summary>
    [Column("category")]
    public required Category Category { get; set; }

    /// <summary>
    /// Продолжительность услуги в минутах
    /// </summary>
    [Column("duration_in_minutes")]
    public required int DurationInMinutes { get; set; }
}
