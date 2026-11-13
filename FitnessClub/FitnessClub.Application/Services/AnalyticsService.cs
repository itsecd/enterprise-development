using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dtos;
using FitnessClub.Contracts.Services;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Application.Services;

/// <summary>Предоставляет результаты аналитических запросов фитнес-клуба.</summary>
public class AnalyticsService(IAnalyticsRepository analyticsRepository, IHallRepository hallRepository) : IAnalyticsService
{
    /// <inheritdoc />
    public async Task<IReadOnlyCollection<TrainerDto>> GetExperiencedTrainersAsync()
    {
        var trainers = await analyticsRepository.GetExperiencedTrainersAsync();
        return [.. trainers.Select(trainer => trainer.ToDto())];
    }

    /// <inheritdoc />
    public async Task<HallAvailabilityDto?> CheckHallAvailabilityAsync(int hallId)
    {
        var hall = await hallRepository.GetByIdAsync(hallId);
        if (hall is null)
        {
            return null;
        }

        return new HallAvailabilityDto
        {
            HallId = hall.Id,
            HallName = hall.Name,
            IsAvailable = !await analyticsRepository.IsHallOccupiedAsync(hallId, DateTime.UtcNow)
        };
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<ClientDto>> GetClientsWithExpiredSubscriptionsAsync()
    {
        var clients = await analyticsRepository.GetExpiredClientsAsync(DateTime.UtcNow.Date);
        return [.. clients.Select(client => client.ToDto())];
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<TrainingDto>> GetCurrentMonthTrainingsByHallAsync(int hallId)
    {
        var today = DateTime.UtcNow.Date;
        var start = new DateTime(today.Year, today.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        var trainings = await analyticsRepository.GetHallTrainingsAsync(hallId, start, start.AddMonths(1));
        return [.. trainings.Select(training => training.ToDto())];
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<TrainerDto>> GetTopFiveTrainersAsync()
    {
        var trainers = await analyticsRepository.GetTopTrainersAsync();
        return [.. trainers.Select(trainer => trainer.ToDto())];
    }
}
