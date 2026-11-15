using FitnessClub.Domain.Data;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Infrastructure.Repositories;

/// <summary>
/// In-memory репозиторий специализаций
/// </summary>
public class SpecializationRepository: IRepository<Specialization, int>
{
    /// <inheritdoc/>
    public Task<Specialization?> Get(int id)
    {
        return Task.FromResult(FitnessClubData.Specializations.FirstOrDefault(specialization => specialization.Id == id));
    }
    /// <inheritdoc/>
    public Task<IReadOnlyList<Specialization>> GetAll()
    {
        return Task.FromResult<IReadOnlyList<Specialization>>(FitnessClubData.Specializations);
    }
    /// <inheritdoc/>
    public Task Add(Specialization entity)
    {
        var specializations = FitnessClubData.Specializations;
        entity.Id = specializations.Count == 0?1:specializations.Max(x => x.Id) + 1;
        specializations.Add(entity);
        return Task.CompletedTask;
    }
    /// <inheritdoc/>
    public Task Update(Specialization entity)
    {
        var specializations = FitnessClubData.Specializations;
        var index = specializations.FindIndex(specialization => specialization.Id == entity.Id);
        if (index == -1)
        {
            throw new KeyNotFoundException($"Специализация с Id: {entity.Id} не найдена");
        }
        specializations[index] = entity;
        return Task.CompletedTask;
    }
    /// <inheritdoc/>
    public async Task<bool> Delete(int id)
    {
        var specialization = await Get(id);
        if (specialization is null)
        {
            return false;
        }
        return FitnessClubData.Specializations.Remove(specialization);
    }
}
