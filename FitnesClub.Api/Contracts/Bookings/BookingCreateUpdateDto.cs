namespace FitnesClub.Api.Contracts.Bookings;

/// <summary>
/// Данные для создания или обновления записи на тренировку.
/// </summary>
/// <param name="MemberId">Идентификатор клиента клуба.</param>
/// <param name="TrainerId">Идентификатор тренера, проводящего занятие.</param>
/// <param name="LessonDateTime">Дата и время начала тренировки.</param>
/// <param name="Duration">Длительность занятия (например, "01:00:00").</param>
/// <param name="RoomName">Название или номер зала/помещения.</param>
/// <param name="IsTrial">Флаг пробного занятия (true — пробное, false — стандартное).</param>
public record BookingCreateUpdateDto(
    Guid MemberId,
    Guid TrainerId,
    DateTime LessonDateTime,
    TimeSpan Duration,
    string RoomName,
    bool IsTrial
);