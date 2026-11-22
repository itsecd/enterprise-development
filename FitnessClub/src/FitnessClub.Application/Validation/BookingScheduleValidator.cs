using FitnessClub.Application.Exceptions;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Validation;

/// <summary>
/// Проверяет доступность расписания для бронирования
/// </summary>
/// <param name="bookingRepository">Репозиторий бронирований</param>
/// <param name="logger">Регистратор событий проверки</param>
public class BookingScheduleValidator(
    IRepository<Booking> bookingRepository,
    ILogger<BookingScheduleValidator> logger)
{
    /// <summary>
    /// Проверяет доступность времени для указанного бронирования
    /// </summary>
    /// <param name="candidate">Бронирование, для которого проверяется доступность времени</param>
    /// <param name="excludingBookingId">Идентификатор бронирования, исключаемого из проверки при обновлении</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Задача асинхронной проверки</returns>
    public async Task EnsureAvailableAsync(
        Booking candidate,
        int? excludingBookingId = null,
        CancellationToken cancellationToken = default)
    {
        var allBookings = await bookingRepository.GetAllAsync(cancellationToken);
        var end = candidate.DateTime + candidate.Duration;
        var overlappingBookings = allBookings
            .Where(existing =>
                existing.Id != excludingBookingId &&
                existing.DateTime < end &&
                candidate.DateTime < existing.DateTime + existing.Duration)
            .ToList();
        if (await bookingRepository.AnyAsync(b =>
                b.Hall.Id == candidate.Hall.Id, cancellationToken))
        {
            logger.LogWarning("Зал {HallId} уже занят в выбранное время", candidate.Hall.Id);
            throw new ConflictException(
                "Зал уже занят в выбранное время.");
        }
        if (await bookingRepository.AnyAsync(b =>
                b.Trainer.Id == candidate.Trainer.Id, cancellationToken))
        {
            logger.LogWarning("У тренера {TrainerId} уже есть занятие в выбранное время", candidate.Trainer.Id);
            throw new ConflictException(
                "У тренера уже есть занятие в выбранное время.");
        }
        if (await bookingRepository.AnyAsync(b =>
                b.Client.Id == candidate.Client.Id, cancellationToken))
        {
            logger.LogWarning("У клиента {ClientId} уже есть занятие в выбранное время", candidate.Client.Id);
            throw new ConflictException(
                "У клиента уже есть занятие в выбранное время.");
        }
        logger.LogInformation("Это время доступно для бронирования");
    }
}