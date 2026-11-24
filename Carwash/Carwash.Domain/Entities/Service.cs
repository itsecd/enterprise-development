using Carwash.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carwash.Domain.Entities;

/// <summary>
/// Класс услуги
/// </summary>
[Table("Services")]
public class Service
{
    /// <summary>
    /// Уникальный идентификатор услуги
    /// </summary>
    [Key]
    [Column("Id")]
    public required int Id { get; set; }

    /// <summary>
    /// Название услуги
    /// </summary>
    [Column("Name")]
    public required string Name { get; set; }

    /// <summary>
    /// Цена услуги
    /// </summary>
    [Column("Price")]
    public required decimal Price { get; set; }

    /// <summary>
    /// Категория автомобиля, для которой предназначена услуга
    /// </summary>
    [Column("Category")]
    public required Category Category { get; set; }

    /// <summary>
    /// Продолжительность услуги в минутах
    /// </summary>
    [Column("DurationInMinutes")]
    public required int DurationInMinutes { get; set; }
}
