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
        var end = candidate.DateTime + candidate.Duration;

        var hallBusy = await bookingRepository.AnyAsync(
            existing => 
                existing.Id != excludingBookingId &&
                existing.Hall.Id == candidate.Hall.Id &&
                existing.DateTime < end &&
                candidate.DateTime < existing.DateTime + existing.Duration,
                cancellationToken);
        if (hallBusy)
        {
            logger.LogWarning("Зал {HallId} уже занят в выбранное время", candidate.Hall.Id);
            throw new ConflictException(
                "Зал уже занят в выбранное время.");
        }
        var trainerBusy = await bookingRepository.AnyAsync(
            existing => 
                existing.Id != excludingBookingId &&
                existing.Trainer.Id == candidate.Trainer.Id &&
                existing.DateTime < end &&
                candidate.DateTime < existing.DateTime + existing.Duration,
                cancellationToken);
        if (trainerBusy)
        {
            logger.LogWarning("У тренера {TrainerId} уже есть занятие в выбранное время", candidate.Trainer.Id);
            throw new ConflictException(
                "У тренера уже есть занятие в выбранное время.");
        }
        var clientBusy = await bookingRepository.AnyAsync(
            existing =>
                existing.Id != excludingBookingId &&
                existing.Client.Id == candidate.Client.Id &&
                existing.DateTime < end &&
                candidate.DateTime < existing.DateTime + existing.Duration,
                cancellationToken);
        if (clientBusy)
        {
            logger.LogWarning("У клиента {ClientId} уже есть занятие в выбранное время", candidate.Client.Id);
            throw new ConflictException(
                "У клиента уже есть занятие в выбранное время.");
        }
        logger.LogInformation("Это время доступно для бронирования");
    }
}