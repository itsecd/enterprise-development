using System.ComponentModel.DataAnnotations;

namespace HotelSystem.Application.Contracts.Rooms;

/// <summary>
/// Данные номера гостиницы для создания и обновления
/// </summary>
public class RoomRequest
{
    /// <summary>
    /// Номер комнаты
    /// </summary>
    [Range(1, 9999)]
    public required int RoomNumber { get; init; }

    /// <summary>
    /// Этаж
    /// </summary>
    [Range(1, 100)]
    public required int Floor { get; init; }

    /// <summary>
    /// Наличие балкона
    /// </summary>
    public bool HasBalcony { get; init; }

    /// <summary>
    /// Идентификатор типа номера
    /// </summary>
    [Range(1, int.MaxValue)]
    public required int RoomTypeId { get; init; }
}