using System.ComponentModel.DataAnnotations;

namespace Hotel.Contracts.Dto;

/// <summary>
/// Данные для создания или изменения бронирования
/// </summary>
public class BookingEditDto
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    [Range(1, int.MaxValue)]
    public int ClientId { get; set; }

    /// <summary>
    /// Идентификатор номера
    /// </summary>
    [Range(1, int.MaxValue)]
    public int RoomId { get; set; }

    /// <summary>
    /// Дата заезда
    /// </summary>
    public DateTime CheckInDate { get; set; }

    /// <summary>
    /// Количество суток проживания
    /// </summary>
    [Range(1, 3650)]
    public int Nights { get; set; }
}
