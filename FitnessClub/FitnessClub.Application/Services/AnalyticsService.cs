using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dtos;
using FitnessClub.Contracts.Services;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Application.Services;

/// <summary>
/// Реализует аналитические запросы фитнес-клуба.
/// </summary>
public class AnalyticsService(
    ITrainerRepository trainerRepository,
    IClientRepository clientRepository,
    ITrainingRepository trainingRepository,
    IHallRepository hallRepository) : IAnalyticsService
{
    /// <inheritdoc />
    public async Task<IReadOnlyCollection<TrainerDto>>
        GetExperiencedTrainersAsync()
    {
        var trainers = await trainerRepository.GetAllAsync();

        return [.. trainers
            .Where(trainer => trainer.WorkExperience >= 5)
            .Select(trainer => trainer.ToDto())];
    }

    /// <inheritdoc />
    public async Task<HallAvailabilityDto?> CheckHallAvailabilityAsync(
        int hallId)
    {
        var hall = await hallRepository.GetByIdAsync(hallId);

        if (hall is null)
        {
            return null;
        }

        var trainings = await trainingRepository.GetAllAsync();
        var now = DateTime.Now;

        var isOccupied = trainings.Any(training =>
            training.Hall.Id == hallId
            && training.StartTime <= now
            && training.StartTime.AddMinutes(training.DurationMinutes) > now);

        return new HallAvailabilityDto
        {
            HallId = hall.Id,
            HallName = hall.Name,
            IsAvailable = !isOccupied
        };
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<ClientDto>>
        GetClientsWithExpiredSubscriptionsAsync()
    {
        var clients = await clientRepository.GetAllAsync();
        var today = DateTime.Today;

        return [.. clients
            .Where(client => client.SubscriptionEndDate.Date < today)
            .OrderBy(client => client.FullName)
            .Select(client => client.ToDto())];
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<TrainingDto>>
        GetCurrentMonthTrainingsByHallAsync(int hallId)
    {
        var hall = await hallRepository.GetByIdAsync(hallId);

        if (hall is null)
        {
            return [];
        }

        var trainings = await trainingRepository.GetAllAsync();
        var now = DateTime.Now;

        return [.. trainings
            .Where(training =>
                training.Hall.Id == hallId
                && training.StartTime.Year == now.Year
                && training.StartTime.Month == now.Month)
            .OrderBy(training => training.StartTime)
            .Select(training => training.ToDto())];
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<TrainerDto>>
        GetTopFiveTrainersAsync()
    {
        var trainers = await trainerRepository.GetAllAsync();
        var trainings = await trainingRepository.GetAllAsync();

        return [.. trainers
            .Select(trainer => new
            {
                Trainer = trainer,
                TrainingCount = trainings.Count(training =>
                    training.Trainer.Id == trainer.Id)
            })
            .Where(item => item.TrainingCount > 0)
            .OrderByDescending(item => item.TrainingCount)
            .ThenBy(item => item.Trainer.FullName)
            .Take(5)
            .Select(item => item.Trainer.ToDto())];
    }
}