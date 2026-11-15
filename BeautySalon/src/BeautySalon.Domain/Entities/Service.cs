using BeautySalon.Shared.Enums;
using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Domain.Entities;

/// <summary>
/// Услуга салона красоты
/// </summary>
public class Service : IEntity
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название услуги
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Категория услуги
    /// </summary>
    public Category Category { get; set; }

    /// <summary>
    /// Цена услуги
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Продолжительность услуги
    /// </summary>
    public TimeSpan Duration { get; set; }
}