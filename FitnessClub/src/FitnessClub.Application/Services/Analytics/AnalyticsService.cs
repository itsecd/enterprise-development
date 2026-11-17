using FitnessClub.Application.Exceptions;
using FitnessClub.Contracts.Dtos.Analytics;
using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services.Analytics;

public sealed class AnalyticsService(
    IAnalyticsQueries analyticsQueries,
    IRepository<Hall> hallRepository,
    ILogger<AnalyticsService> logger) : IAnalyticsService
{
    public async Task<IReadOnlyList<TrainerDto>> GetExperiencedTrainersAsync(
        int minimumExperience = 5, 
        CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(minimumExperience);
        if (logger.IsEnabled(LogLevel.Debug)) 
        {
            logger.LogDebug("Поиск тренеров со стажем от {MinimumExperience} лет", minimumExperience);
        }
        return await analyticsQueries.GetExperiencedTrainersAsync(minimumExperience, cancellationToken);
    }

    public async Task<IReadOnlyList<ClientDto>> GetClientsWithOverdueSubscriptionsAsync(
        DateOnly asOfDate, 
        CancellationToken cancellationToken = default)
    {
        if (logger.IsEnabled(LogLevel.Debug)) 
        {
            logger.LogDebug("Поиск клиентов с просроченной подпиской на {AsOfDate}", asOfDate);
        }
        return await analyticsQueries.GetClientsWithOverdueSubscriptionsAsync(asOfDate, cancellationToken);
    }

    public async Task<IReadOnlyList<PopularTrainerDto>> GetMostPopularTrainersAsync(
        int count = 5, 
        CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
        if (logger.IsEnabled(LogLevel.Debug)) 
        {
            logger.LogDebug("Поиск {Count} самых популярных тренеров", count);
        }
        return await analyticsQueries.GetMostPopularTrainersAsync(count, cancellationToken);
    }

    public async Task<bool> IsHallAvailableAsync(
        int hallId, 
        DateTime dateTime, 
        CancellationToken cancellationToken = default)
    {
        await EnsureHallExistsAsync(hallId, cancellationToken);
        if (logger.IsEnabled(LogLevel.Debug)) 
        {
            logger.LogDebug("Проверка доступности зала {HallId} на {DateTime}", hallId, dateTime);
        }
        return await analyticsQueries.IsHallAvailableAsync(hallId, dateTime, cancellationToken);
    }

    public async Task<IReadOnlyList<BookingDto>> GetBookingsInHallForMonthAsync(
        int hallId, 
        DateOnly dateInMonth, 
        CancellationToken cancellationToken = default)
    {
        await EnsureHallExistsAsync(hallId, cancellationToken);
        if (logger.IsEnabled(LogLevel.Debug)) 
        {
            logger.LogDebug("Поиск бронирований зала {HallId} за месяц {DateInMonth}", hallId, dateInMonth);
        }
        return await analyticsQueries.GetHallBookingsForMonthAsync(hallId, dateInMonth, cancellationToken);
    }

    private async Task EnsureHallExistsAsync(
        int hallId, 
        CancellationToken cancellationToken)
    {
        var hall = await hallRepository.GetByIdAsync(hallId, cancellationToken);
        if (hall is not null)
            return;

        logger.LogWarning("Зал {HallId} не найден", hallId);
        throw new NotFoundException($"Зал с ID {hallId} не найден.");
    }
}