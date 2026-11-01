using AutoServiceLab.Domain.Enums;

namespace AutoServiceLab.Domain.Entities;

/// <summary>
/// Вид работ
/// </summary>
public class WorkType
{
    /// <summary>
    /// Идентификатор работы
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название работы
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Категория работы
    /// </summary>
    public required WorkCategory Category { get; set; }

    /// <summary>
    /// Продолжительность работы в часах
    /// </summary>
    public decimal Duration { get; set; }

    /// <summary>
    /// Стоимость работы
    /// </summary>
    public decimal LaborCost { get; set; }

    /// <summary>
    /// Заказы
    /// </summary>
    public List<OrderWork> OrderWorks { get; set; } = [];
}
