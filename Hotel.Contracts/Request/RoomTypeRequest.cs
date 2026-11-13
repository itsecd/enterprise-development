using Hotel.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Contracts.Request;

/// <summary>
/// Обработка запросов на создание/обновление типа комнаты
/// </summary>
public sealed class RoomTypeRequest
{
    /// <summary>
    /// Категория номера
    /// </summary>
    [Required]
    public RoomCategory? Category { get; init; }

    /// <summary>
    /// Площадь комнаты
    /// </summary>
    [Required, Range(1, 1000)]
    public int? RoomArea { get; init; }

    /// <summary>
    /// Количество кроватей
    /// </summary>
    [Required, Range(1, 20)]
    public int? BedCount { get; init; }

    /// <summary>
    /// Наличие ванны/душа
    /// </summary>
    [Required]
    public bool? HasBathroom { get; init; }

    /// <summary>
    /// Цена за сутки
    /// </summary>
    [Required, Range(1, 10_000_000)]
    public decimal? Price { get; init; }
}