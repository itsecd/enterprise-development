using CarWash.Domain.Shared.Enums;

namespace CarWash.Domain.Entities;

/// <summary>
/// Услуга автомойки
/// </summary>
public class CarWashProcedure
{
    /// <summary>
    /// Уникальный идентификатор услуги
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название услуги
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Категория автомобиля, для которой предназначена услуга
    /// </summary>
    public CarCategory CarCategory { get; set; }

    /// <summary>
    /// Стоимость услуги
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Продолжительность услуги
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Заказы, в которых была оказана услуга
    /// </summary>
    public List<Order> Orders { get; set; } = [];
}