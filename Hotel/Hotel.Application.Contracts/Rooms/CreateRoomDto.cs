using System.ComponentModel.DataAnnotations;

namespace Hotel.Application.Contracts.Rooms;

/// <summary>
/// DTO для создания номера
/// </summary>
public record CreateRoomDto(
    [Range(1, int.MaxValue, ErrorMessage = "Номер комнаты должен быть положительным")]
    int RoomNumber,

    int Floor,

    bool HasBalcony,

    [Range(1, int.MaxValue, ErrorMessage = "Идентификатор типа номера должен быть положительным")]
    int RoomTypeId
);
