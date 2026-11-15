using AutoServiceLab.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoServiceLab.Domain.Entities;

/// <summary>
/// Вид работ
/// </summary>
[Table("work_types")]
public class WorkType
{
    /// <summary>
    /// Идентификатор работы
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Название работы
    /// </summary>
    [Column("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Категория работы
    /// </summary>
    [Column("category")]
    public required WorkCategory Category { get; set; }

    /// <summary>
    /// Продолжительность работы в часах
    /// </summary>
    [Column("duration")]
    public decimal Duration { get; set; }

    /// <summary>
    /// Стоимость работы
    /// </summary>
    [Column("labor_cost")]
    public decimal LaborCost { get; set; }

    /// <summary>
    /// Заказы
    /// </summary>
    public List<OrderWork> OrderWorks { get; set; } = [];
}
