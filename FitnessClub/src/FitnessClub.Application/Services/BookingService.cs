using FitnessClub.Application.Mapping;
using FitnessClub.Application.Exceptions;
using FitnessClub.Contracts.Dtos.Edit;
using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Application.Validation;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Управление бронированиями
/// </summary>
public class BookingService(
    IRepository<Booking> bookingRepository,
    IRepository<Client> clientRepository,
    IRepository<Trainer> trainerRepository,
    IRepository<Hall> hallRepository,
    BookingScheduleValidator scheduleValidator,
    ILogger<BookingService> logger
    ) : ICrudService<BookingDto, BookingEditDto>
{
    /// <summary>
    /// Получить список бронирований
    /// </summary>
    public async Task<IReadOnlyList<BookingDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var bookings = await bookingRepository.GetAllAsync(cancellationToken);
        return [.. bookings.Select(b => b.ToDto())];
    }
    /// <summary>
    /// Получить бронирование по идентификатору
    /// </summary>
    public async Task<BookingDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var booking = await bookingRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException($"Бронирование с ID {id} не найдено.");
        return booking.ToDto();
    }
    /// <summary>
    /// Создать бронирование с проверкой доступности
    /// </summary>
    public async Task<BookingDto> AddAsync(BookingEditDto editDto, CancellationToken cancellationToken = default)
    {
        var (client, trainer, hall) = await GetBookingReferencesAsync(editDto, cancellationToken);
        var booking = editDto.ToEntity(client, trainer, hall);
        await scheduleValidator.EnsureAvailableAsync(booking, cancellationToken: cancellationToken);

        var savedBooking = await bookingRepository.AddAsync(booking, cancellationToken);
        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation(
                "Создано бронирование {BookingId}: клиент {ClientId}, тренер {TrainerId}, зал {HallId}",
                savedBooking.Id,
                savedBooking.Client.Id,
                savedBooking.Trainer.Id,
                savedBooking.Hall.Id);
        }
        return savedBooking.ToDto();
    }
    /// <summary>
    /// Обновить бронирование с проверкой доступности
    /// </summary>
    public async Task<BookingDto> UpdateAsync(int id, BookingEditDto editDto, CancellationToken cancellationToken = default)
    {
        var existingBooking = await bookingRepository.GetByIdAsync(id, cancellationToken);
        if (existingBooking is null)
        {
            logger.LogWarning("Бронирование {BookingId} не найдено для обновления", id);
            throw new NotFoundException($"Бронирование с ID {id} не найдено.");
        }
        var (client, trainer, hall) = await GetBookingReferencesAsync(editDto, cancellationToken);
        var candidateBooking = editDto.ToEntity(client, trainer, hall);
        await scheduleValidator.EnsureAvailableAsync(candidateBooking, excludingBookingId: id, cancellationToken: cancellationToken);
        existingBooking.UpdateEntity(editDto, client, trainer, hall);
        await bookingRepository.UpdateAsync(existingBooking, cancellationToken);
        if (logger.IsEnabled(LogLevel.Information))
            logger.LogInformation("Обновлено бронирование {BookingId}", id);

        return existingBooking.ToDto();
    }
    /// <summary>
    /// Удалить бронирование по идентификатору
    /// </summary>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var deleted = await bookingRepository.DeleteAsync(id, cancellationToken);
        if (deleted)
            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Удалено бронирование {BookingId}", id);
        else
            logger.LogWarning("Бронирование {BookingId} не найдено для удаления", id);
        return deleted;
    }
    /// <summary>
    /// Получить клиента, тренера и зал для брони
    /// </summary>
    private async Task<(Client, Trainer, Hall)> GetBookingReferencesAsync(
        BookingEditDto editDto,
        CancellationToken cancellationToken)
    {
        var client = await clientRepository.GetByIdAsync(editDto.ClientId, cancellationToken)
            ?? throw new NotFoundException($"Клиент с ID {editDto.ClientId} не найден.");
        var trainer = await trainerRepository.GetByIdAsync(editDto.TrainerId, cancellationToken)
            ?? throw new NotFoundException($"Тренер с ID {editDto.TrainerId} не найден.");
        var hall = await hallRepository.GetByIdAsync(editDto.HallId, cancellationToken)
            ?? throw new NotFoundException($"Зал с ID {editDto.HallId} не найден.");
        return (client, trainer, hall);
    }
}