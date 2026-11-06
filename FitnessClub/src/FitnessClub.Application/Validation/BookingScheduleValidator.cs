using FitnessClub.Application.Exceptions;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Validation;

public class BookingScheduleValidator(
    IRepository<Booking> bookingRepository,
    ILogger<BookingScheduleValidator> logger)
{
    public async Task EnsureAvailableAsync(
        Booking candidate,
        int? excludingBookingId = null)
    {
        var allBookings = await bookingRepository.GetAllAsync();
        var end = candidate.DateTime + candidate.Duration;
        var overlappingBookings = allBookings
            .Where(existing =>
                existing.Id != excludingBookingId &&
                existing.DateTime < end &&
                candidate.DateTime < existing.DateTime + existing.Duration)
            .ToList();
        if (overlappingBookings.Any(b =>
                b.Hall.Id == candidate.Hall.Id))
        {
            logger.LogWarning("Зал {HallId} уже занят в выбранное время", candidate.Hall.Id);
            throw new ConflictException(
                "Зал уже занят в выбранное время.");
        }
        if (overlappingBookings.Any(b =>
                b.Trainer.Id == candidate.Trainer.Id))
        {
            logger.LogWarning("У тренера {TrainerId} уже есть занятие в выбранное время", candidate.Trainer.Id);
            throw new ConflictException(
                "У тренера уже есть занятие в выбранное время.");
        }
        if (overlappingBookings.Any(b =>
                b.Client.Id == candidate.Client.Id))
        {
            logger.LogWarning("У клиента {ClientId} уже есть занятие в выбранное время", candidate.Client.Id);
            throw new ConflictException(
                "У клиента уже есть занятие в выбранное время.");
        }
        logger.LogInformation("Это время доступно для бронирования");
    }
}