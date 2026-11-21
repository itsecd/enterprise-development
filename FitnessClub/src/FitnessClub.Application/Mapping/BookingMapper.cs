using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Dtos.Edit;
using FitnessClub.Domain.Entities;

namespace FitnessClub.Application.Mapping;
/// <summary>
/// Преобразует данные бронирований
/// </summary>
public static class BookingMapper
{
    /// <summary>
    /// Преобразует запись в DTO
    /// </summary>
    public static BookingDto ToDto(this Booking booking) =>
        new(
            Id: booking.Id,
            ClientId: booking.Client.Id,
            TrainerId: booking.Trainer.Id,
            HallId: booking.Hall.Id,
            DateTime: booking.DateTime,
            IsTrial: booking.IsTrial,
            Duration: booking.Duration);
    /// <summary>
    /// Создаёт запись из DTO и связанных сущностей
    /// </summary>
    public static Booking ToEntity(this BookingEditDto bookingEditDto, Client client, Trainer trainer, Hall hall) =>
        new()
        {
            Client = client,
            Trainer = trainer,
            Hall = hall,
            DateTime = bookingEditDto.DateTime,
            IsTrial = bookingEditDto.IsTrial,
            Duration = bookingEditDto.Duration
        };
    /// <summary>
    /// Обновляет запись данными из DTO и связанными сущностями
    /// </summary>
    public static void UpdateEntity(this Booking booking, BookingEditDto bookingEditDto, Client client, Trainer trainer, Hall hall)
    {
        booking.Client = client;
        booking.Trainer = trainer;
        booking.Hall = hall;
        booking.DateTime = bookingEditDto.DateTime;
        booking.IsTrial = bookingEditDto.IsTrial;
        booking.Duration = bookingEditDto.Duration;
    }
}