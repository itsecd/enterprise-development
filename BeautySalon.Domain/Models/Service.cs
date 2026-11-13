using BeautySalon.Enums;

namespace BeautySalon.Domain.Models;

/// <summary>
/// Услуги салона красоты
/// </summary>
public class Service
{
    /// <summary>
    /// Уникальный идентификатор услуги
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Название услуги
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Категория услуги
    /// </summary>
    public required ServiceCategory Category { get; set; }

    /// <summary>
    /// Стоимость услуги
    /// </summary>
    public required decimal Price { get; set; }

    /// <summary>
    /// Длительность услуги в минутах
    /// </summary>
    public required TimeSpan Duration { get; set; }

    /// <summary>
    /// Строковое представление
    /// </summary>
    public override string ToString() =>
    $"Name: {Name}, Category: {Category}, Price: {Price}, Duration: {Duration}";
}