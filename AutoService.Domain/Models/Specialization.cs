namespace AutoService.Domain.Models;

/// <summary>
/// Специализация механика автосервиса
/// </summary>
public enum Specialization
{
    /// <summary>
    /// Двигатель
    /// </summary>
    Engine,

    /// <summary>
    /// Трансмиссия
    /// </summary>
    Transmission,

    /// <summary>
    /// Электрика
    /// </summary>
    Electrical,

    /// <summary>
    /// Кузов
    /// </summary>
    Body,

    /// <summary>
    /// Диагностика
    /// </summary>
    Diagnostics,

    /// <summary>
    /// Подвеска
    /// </summary>
    Suspension,

    /// <summary>
    /// Тормоза
    /// </summary>
    Brakes,

    /// <summary>
    /// Покраска
    /// </summary>
    Painting
}