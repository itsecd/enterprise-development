namespace FitnesClub.Api.Contracts.Bookings;

public record BookingCreateUpdateDto(
    Guid MemberId,
    Guid TrainerId,
    DateTime LessonDateTime,
    TimeSpan Duration,
    string RoomName,
    bool IsTrial
);