using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.EfCore.Repositories;

/// <summary>Фильтрует, сортирует и группирует данные средствами PostgreSQL.</summary>
public class AnalyticsRepository(FitnessClubDbContext context) : IAnalyticsRepository
{
    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Trainer>> GetExperiencedTrainersAsync() =>
        await context.Trainers.AsNoTracking().Where(trainer => trainer.WorkExperience >= 5)
            .OrderBy(trainer => trainer.Id).ToListAsync();

    /// <inheritdoc />
    public Task<bool> IsHallOccupiedAsync(int hallId, DateTime now) =>
        context.Trainings.AnyAsync(training => training.HallId == hallId
            && training.StartTime <= now
            && training.StartTime.AddMinutes(training.DurationMinutes) > now);

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Client>> GetExpiredClientsAsync(DateTime today) =>
        await context.Clients.AsNoTracking().Where(client => client.SubscriptionEndDate < today)
            .OrderBy(client => client.FullName).ToListAsync();

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Training>> GetHallTrainingsAsync(
        int hallId, DateTime start, DateTime end) =>
        await context.Trainings.AsNoTracking()
            .Where(training => training.HallId == hallId && training.StartTime >= start && training.StartTime < end)
            .OrderBy(training => training.StartTime).ToListAsync();

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Trainer>> GetTopTrainersAsync() =>
        await context.Trainers.AsNoTracking()
            .Where(trainer => context.Trainings.Any(training => training.TrainerId == trainer.Id))
            .OrderByDescending(trainer => context.Trainings.Count(training => training.TrainerId == trainer.Id))
            .ThenBy(trainer => trainer.FullName).ThenBy(trainer => trainer.Id)
            .Take(5).ToListAsync();
}
