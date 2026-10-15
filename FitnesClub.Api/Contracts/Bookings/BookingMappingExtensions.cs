using FitnesClub.Domain.Entities;

namespace FitnesClub.Api.Contracts.Bookings;

public static class BookingMappingExtensions
{
    public static BookingDto ToDto(this Booking b) => new(
        b.Id,
        b.MemberId,
        b.TrainerId,
        b.LessonDateTime,
        b.Duration,
        b.RoomName,
        b.IsTrial
    );

    public static Booking ToEntity(this BookingCreateUpdateDto dto) => new()
    {
        Id = Guid.NewGuid(),
        MemberId = dto.MemberId,
        TrainerId = dto.TrainerId,
        LessonDateTime = dto.LessonDateTime,
        Duration = dto.Duration,
        RoomName = dto.RoomName,
        IsTrial = dto.IsTrial,
    };

    public static void UpdateEntity(this BookingCreateUpdateDto dto, Booking b)
    {
        b.MemberId = dto.MemberId;
        b.TrainerId = dto.TrainerId;
        b.LessonDateTime = dto.LessonDateTime;
        b.Duration = dto.Duration;
        b.RoomName = dto.RoomName;
        b.IsTrial = dto.IsTrial;
    }
}