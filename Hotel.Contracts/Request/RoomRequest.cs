using System.ComponentModel.DataAnnotations;

namespace Hotel.Contracts.Request;

/// <summary>
/// Обработка запросов на создание/обновление комнаты
/// </summary>
public sealed class RoomRequest
{
    /// <summary>
    /// Номер комнаты
    /// </summary>
    [Required, Range(1, 10000)]
    public int? Number { get; init; }

    /// <summary>
    /// Этаж
    /// </summary>
    [Required, Range(-5, 200)]
    public int? Floor { get; init; }

    /// <summary>
    /// Наличие балкона
    /// </summary>
    [Required]
    public bool? HasBalcony { get; init; }

    /// <summary>
    /// Id типа комнаты
    /// </summary>
    [Required, Range(1, int.MaxValue)]
    public int? RoomTypeId { get; init; }
}