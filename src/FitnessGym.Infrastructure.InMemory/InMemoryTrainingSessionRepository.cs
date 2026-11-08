using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Interfaces;

namespace FitnessGym.Infrastructure.InMemory;

/// <summary>
/// Репозиторий сущностей TrainingSession с хранением в памяти.
/// </summary>
/// <param name="seed">Начальные данные.</param>
public class InMemoryTrainingSessionRepository(IReadOnlyList<TrainingSession> seed)
    : InMemoryRepository<TrainingSession>(seed), ITrainingSessionRepository
{
    /// <inheritdoc/>
    public Task<IReadOnlyList<TrainingSession>> GetForMonthAsync(string hallName, DateTime month) =>
        ReadAsync<IReadOnlyList<TrainingSession>>(items =>
            [.. items.Where(session => session.HallName == hallName
                                      && session.StartsAt.Year == month.Year
                                      && session.StartsAt.Month == month.Month)]);

    /// <inheritdoc/>
    public Task<bool> HasClientAsync(int clientId) =>
        ReadAsync(items => items.Any(session => session.Client.Id == clientId));

    /// <inheritdoc/>
    public Task<bool> HasTrainerAsync(int trainerId) =>
        ReadAsync(items => items.Any(session => session.Trainer.Id == trainerId));

    /// <inheritdoc/>
    public Task<IReadOnlyList<Trainer>> GetPopularTrainersAsync(int count) =>
        ReadAsync<IReadOnlyList<Trainer>>(items =>
            [.. items.GroupBy(session => session.Trainer.Id)
                .OrderByDescending(group => group.Count())
                .Take(count)
                .Select(group => group.First().Trainer)]);

    /// <inheritdoc/>
    public Task<IReadOnlyList<string>> GetHallsAsync() =>
        ReadAsync<IReadOnlyList<string>>(items => [.. items.Select(session => session.HallName).Distinct()]);

    /// <inheritdoc/>
    public Task<bool> IsHallAvailableAsync(string hallName, DateTime at) =>
        ReadAsync(items => !items.Any(session => session.HallName == hallName
                                                && session.EndsAt != null
                                                && session.StartsAt <= at && at < session.EndsAt));

    /// <inheritdoc/>
    protected override void CopyProperties(TrainingSession source, TrainingSession target)
    {
        target.Client = source.Client;
        target.Trainer = source.Trainer;
        target.ClientId = source.ClientId;
        target.TrainerId = source.TrainerId;
        target.StartsAt = source.StartsAt;
        target.EndsAt = source.EndsAt;
        target.HallName = source.HallName;
        target.IsTrial = source.IsTrial;
    }
}
