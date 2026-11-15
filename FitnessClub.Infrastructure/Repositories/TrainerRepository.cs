using FitnessClub.Domain.Data;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Infrastructure.Repositories;

/// <summary>
/// In-memory репозиторий тренеров
/// </summary>
public class TrainerRepository: ITrainerRepository
{
    /// <inheritdoc/>
    public Task<Trainer?> Get(int id)
    {
        return Task.FromResult(FitnessClubData.Trainers.FirstOrDefault(trainer => trainer.Id == id));
    }
    /// <inheritdoc/>
    public Task<IReadOnlyList<Trainer>> GetAll()
    {
        return Task.FromResult<IReadOnlyList<Trainer>>(FitnessClubData.Trainers);
    }
    /// <inheritdoc/>
    public Task Add(Trainer entity)
    {
        var trainers = FitnessClubData.Trainers;
        entity.Id = trainers.Count == 0?1:trainers.Max(x => x.Id) + 1;
        trainers.Add(entity);
        return Task.CompletedTask;
    }
    /// <inheritdoc/>
    public Task Update(Trainer entity)
    {
        var trainers = FitnessClubData.Trainers;
        var index = trainers.FindIndex(trainer => trainer.Id == entity.Id);
        if (index == -1)
        {
            throw new KeyNotFoundException($"Тренер с Id: {entity.Id} не найден");
        }
        trainers[index] = entity;
        return Task.CompletedTask;
    }
    /// <inheritdoc/>
    public async Task<bool> Delete(int id)
    {
        var trainer = await Get(id);
        if (trainer is null)
        {
            return false;
        }
        return FitnessClubData.Trainers.Remove(trainer);
    }
    /// <inheritdoc/>
    public async Task<bool> HasTrainersForSpecialization(int specializationId)
    {
        return (await GetAll()).Any(t => t.Specialization.Id == specializationId);
    }
    /// <inheritdoc/>
    public async Task<IReadOnlyList<Trainer>> GetBySpecialization(int specializationId)
    {
        return (await GetAll()).Where(t => t.Specialization.Id == specializationId).ToList();
    }
    /// <inheritdoc/>
    public async Task<IReadOnlyList<Trainer>> GetWithMinExperience(int minYears)
    {
        return (await GetAll()).Where(t => t.WorkYears >= minYears).ToList();
    }
    /// <inheritdoc/>
    public async Task<IReadOnlyList<Trainer>> GetByIds(IReadOnlyCollection<int> ids)
    {
        return (await GetAll()).Where(t => ids.Contains(t.Id)).ToList();
    }
}