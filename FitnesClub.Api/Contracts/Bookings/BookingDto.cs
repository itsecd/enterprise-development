namespace FitnesClub.Api.Contracts.Bookings;

/// <summary>
/// Информация о существующей записи на тренировку.
/// </summary>
/// <param name="Id">Уникальный идентификатор записи.</param>
/// <param name="MemberId">Идентификатор клиента клуба.</param>
/// <param name="TrainerId">Идентификатор назначенного тренера.</param>
/// <param name="LessonDateTime">Дата и время начала занятия.</param>
/// <param name="Duration">Длительность занятия.</param>
/// <param name="RoomName">Название или номер зала проведения.</param>
/// <param name="IsTrial">Признак того, является ли тренировка пробной.</param>
public record BookingDto(
    Guid Id,
    Guid MemberId,
    Guid TrainerId,
    DateTime LessonDateTime,
    TimeSpan Duration,
    string RoomName,
    bool IsTrial
);