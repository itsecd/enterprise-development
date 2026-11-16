using System.ComponentModel.DataAnnotations;

namespace Hotel.Application.Contracts.Rooms;

/// <summary>
/// DTO для создания номера
/// </summary>
/// <param name="RoomNumber">Номер комнаты</param>
/// <param name="Floor">Этаж</param>
/// <param name="HasBalcony">Наличие балкона</param>
/// <param name="RoomTypeId">Идентификатор типа номера</param>
public record CreateRoomDto(
    [Range(1, int.MaxValue, ErrorMessage = "Номер комнаты должен быть положительным")]
    int RoomNumber,

    int Floor,

    bool HasBalcony,

    [Range(1, int.MaxValue, ErrorMessage = "Идентификатор типа номера должен быть положительным")]
    int RoomTypeId
);
