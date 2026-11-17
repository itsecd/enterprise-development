using System.ComponentModel.DataAnnotations;

namespace Hotel.Application.Contracts.Bookings;

/// <summary>
/// DTO для создания бронирования
/// </summary>
/// <param name="ClientId">Идентификатор клиента</param>
/// <param name="RoomId">Идентификатор номера</param>
/// <param name="CheckInDate">Дата заезда</param>
/// <param name="DaysCount">Количество дней проживания</param>
public record CreateBookingDto(
    [Range(1, int.MaxValue, ErrorMessage = "Идентификатор клиента должен быть положительным")]
    int ClientId,

    [Range(1, int.MaxValue, ErrorMessage = "Идентификатор номера должен быть положительным")]
    int RoomId,

    DateOnly CheckInDate,

    [Range(1, int.MaxValue, ErrorMessage = "Количество дней должно быть положительным")]
    int DaysCount
);
