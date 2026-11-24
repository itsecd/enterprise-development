using System.ComponentModel.DataAnnotations;

namespace Hotel.Contracts.Dto;

/// <summary>
/// Данные для создания или изменения номера
/// </summary>
public class RoomEditDto
{
    /// <summary>
    /// Номер комнаты
    /// </summary>
    [Required, MaxLength(20)]
    public required string RoomNumber { get; set; }

    /// <summary>
    /// Этаж
    /// </summary>
    [Range(1, 200)]
    public int Floor { get; set; }

    /// <summary>
    /// Наличие балкона
    /// </summary>
    public bool HasBalcony { get; set; }

    /// <summary>
    /// Идентификатор типа номера
    /// </summary>
    [Range(1, int.MaxValue)]
    public int RoomTypeId { get; set; }
}
