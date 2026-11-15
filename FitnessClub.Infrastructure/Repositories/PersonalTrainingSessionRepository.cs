using FitnessClub.Domain.Data;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Infrastructure.Repositories;

/// <summary>
/// In-memory репозиторий персональных тренировок
/// </summary>
public class PersonalTrainingSessionRepository: IPersonalTrainingSessionRepository
{
    /// <inheritdoc/>
    public Task<PersonalTrainingSession?> Get(int id)
    {
        return Task.FromResult(FitnessClubData.PersonalTrainingSessions.FirstOrDefault(session => session.Id == id));
    }
    /// <inheritdoc/>
    public Task<IReadOnlyList<PersonalTrainingSession>> GetAll()
    {
        return Task.FromResult<IReadOnlyList<PersonalTrainingSession>>(FitnessClubData.PersonalTrainingSessions);
    }
    /// <inheritdoc/>
    public Task Add(PersonalTrainingSession entity)
    {
        var sessions = FitnessClubData.PersonalTrainingSessions;
        entity.Id = sessions.Count == 0 ? 1 : sessions.Max(x => x.Id) + 1;
        sessions.Add(entity);
        return Task.CompletedTask;
    }
    /// <inheritdoc/>
    public Task Update(PersonalTrainingSession entity)
    {
        var sessions = FitnessClubData.PersonalTrainingSessions;
        var index = sessions.FindIndex(session => session.Id == entity.Id);
        if (index == -1)
        {
            throw new KeyNotFoundException($"Персональная тренировка с Id: {entity.Id} не найдена");
        }
        sessions[index] = entity;
        return Task.CompletedTask;
    }
    /// <inheritdoc/>
    public async Task<bool> Delete(int id)
    {
        var session = await Get(id);
        if (session is null)
        {
            return false;
        }
        return FitnessClubData.PersonalTrainingSessions.Remove(session);
    }
    /// <inheritdoc/>
    public async Task<bool> HasSessionsForClient(int clientId)
    {
        return (await GetAll()).Any(s => s.Client.Id == clientId);
    }
    /// <inheritdoc/>
    public async Task<bool> HasSessionsForTrainer(int trainerId)
    {
        return (await GetAll()).Any(s => s.Trainer.Id == trainerId);
    }
    /// <inheritdoc/>
    public async Task<IReadOnlyList<PersonalTrainingSession>> GetByClient(int clientId)
    {
        return (await GetAll()).Where(s => s.Client.Id == clientId).ToList();
    }
    /// <inheritdoc/>
    public async Task<IReadOnlyList<PersonalTrainingSession>> GetByTrainer(int trainerId)
    {
        return (await GetAll()).Where(s => s.Trainer.Id == trainerId).ToList();
    }
    /// <inheritdoc/>
    public async Task<bool> HallExists(string hallName)
    {
        return (await GetAll()).Any(s => s.HallName == hallName);
    }
    /// <inheritdoc/>
    public async Task<bool> HasSessionInHall(string hallName, DateTime checkTime)
    {
        return (await GetAll()).Any(s =>s.HallName == hallName && s.TrainDay == checkTime);
    }
    /// <inheritdoc/>
    public async Task<IReadOnlyList<PersonalTrainingSession>> GetByHallAndPeriod(
        string hallName, DateTime start, DateTime end)
    {
        return (await GetAll())
            .Where(s => s.HallName == hallName &&
                        s.TrainDay >= start &&
                        s.TrainDay < end)
            .ToList();
    }
    /// <inheritdoc/>
    public async Task<IReadOnlyList<int>> GetTopFiveTrainerIds()
    {
        return (await GetAll())
            .GroupBy(s => s.Trainer.Id)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key)
            .Take(5)
            .Select(g => g.Key)
            .ToList();
    }
}