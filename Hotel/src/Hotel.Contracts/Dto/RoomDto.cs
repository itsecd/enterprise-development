namespace Hotel.Contracts.Dto;

/// <summary>
/// Данные гостиничного номера в ответе API
/// </summary>
public class RoomDto
{
    /// <summary>
    /// Идентификатор номера
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер комнаты
    /// </summary>
    public required string RoomNumber { get; set; }

    /// <summary>
    /// Этаж
    /// </summary>
    public int Floor { get; set; }

    /// <summary>
    /// Наличие балкона
    /// </summary>
    public bool HasBalcony { get; set; }

    /// <summary>
    /// Тип номера
    /// </summary>
    public required RoomTypeDto Type { get; set; }
}
