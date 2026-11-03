namespace HotelSystem.Application.Contracts.Rooms;

/// <summary>
/// Данные номера гостиницы, возвращаемые API
/// </summary>
public class RoomResponse : IEntityResponse
{
    /// <summary>
    /// Идентификатор номера
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Номер комнаты
    /// </summary>
    public int RoomNumber { get; init; }

    /// <summary>
    /// Этаж
    /// </summary>
    public int Floor { get; init; }

    /// <summary>
    /// Наличие балкона
    /// </summary>
    public bool HasBalcony { get; init; }

    /// <summary>
    /// Идентификатор типа номера
    /// </summary>
    public int RoomTypeId { get; init; }

    /// <summary>
    /// Категория номера
    /// </summary>
    public string RoomCategory { get; init; } = string.Empty;
}