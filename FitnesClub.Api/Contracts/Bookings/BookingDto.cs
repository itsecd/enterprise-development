namespace FitnesClub.Api.Contracts.Bookings;

public record BookingDto(
    Guid Id,
    Guid MemberId,
    Guid TrainerId,
    DateTime LessonDateTime,
    TimeSpan Duration,
    string RoomName,
    bool IsTrial
);