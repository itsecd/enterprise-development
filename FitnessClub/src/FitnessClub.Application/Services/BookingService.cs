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
    public async Task<IReadOnlyList<BookingDto>> GetAllAsync()
    {
        var bookings = await bookingRepository.GetAllAsync();
        return [.. bookings.Select(b => b.ToDto())];
    }
    /// <summary>
    /// Получить бронирование по идентификатору
    /// </summary>
    public async Task<BookingDto?> GetByIdAsync(int id)
    {
        var booking = await bookingRepository.GetByIdAsync(id);
        return booking?.ToDto();
    }
    /// <summary>
    /// Создать бронирование с проверкой доступности
    /// </summary>
    public async Task<BookingDto> AddAsync(BookingEditDto editDto)
    {
        var (client, trainer, hall) = await GetBookingReferencesAsync(editDto);
        var booking = editDto.ToEntity(client, trainer, hall);
        await scheduleValidator.EnsureAvailableAsync(booking);

        var savedBooking = await bookingRepository.AddAsync(booking);
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
    public async Task<BookingDto?> UpdateAsync(int id, BookingEditDto editDto)
    {
        var existingBooking = await bookingRepository.GetByIdAsync(id);
        if (existingBooking is null)
        {
            logger.LogWarning("Бронирование {BookingId} не найдено для обновления", id);
            return null;
        }
        var (client, trainer, hall) = await GetBookingReferencesAsync(editDto);
        var candidateBooking = editDto.ToEntity(client, trainer, hall);
        await scheduleValidator.EnsureAvailableAsync(candidateBooking, excludingBookingId: id);
        existingBooking.UpdateEntity(editDto, client, trainer, hall);
        await bookingRepository.UpdateAsync(existingBooking);
        if (logger.IsEnabled(LogLevel.Information))
            logger.LogInformation("Обновлено бронирование {BookingId}", id);

        return existingBooking.ToDto();
    }
    /// <summary>
    /// Удалить бронирование по идентификатору
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var deleted = await bookingRepository.DeleteAsync(id);
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
        BookingEditDto editDto)
    {
        var client = await clientRepository.GetByIdAsync(editDto.ClientId)
            ?? throw new NotFoundException($"Клиент с ID {editDto.ClientId} не найден.");
        var trainer = await trainerRepository.GetByIdAsync(editDto.TrainerId)
            ?? throw new NotFoundException($"Тренер с ID {editDto.TrainerId} не найден.");
        var hall = await hallRepository.GetByIdAsync(editDto.HallId)
            ?? throw new NotFoundException($"Зал с ID {editDto.HallId} не найден.");
        return (client, trainer, hall);
    }
}