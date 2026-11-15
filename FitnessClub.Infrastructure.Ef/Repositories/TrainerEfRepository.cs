using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.Ef.Repositories;

/// <summary>
/// Репозиторий тренеров с хранением в БД
/// </summary>
public class TrainerEfRepository(FitnessClubDbContext context): ITrainerRepository
{
    /// <inheritdoc/>
    public async Task<Trainer?> Get(int id)
    {
        return await context.Trainers.FirstOrDefaultAsync(t => t.Id == id);
    }
    /// <inheritdoc/>
    public async Task<IReadOnlyList<Trainer>> GetAll()
    {
        return await context.Trainers.ToListAsync();
    }
    /// <inheritdoc/>
    public async Task Add(Trainer entity)
    {
        var specialization = await context.Specializations.FindAsync(entity.Specialization.Id)?? throw new ArgumentException("Специализация не найдена");
        entity.Specialization = specialization;
        context.Trainers.Add(entity);
        await context.SaveChangesAsync();
    }
    /// <inheritdoc/>
    public async Task Update(Trainer entity)
    {
        var trainer = await context.Trainers.FirstOrDefaultAsync(t => t.Id == entity.Id)?? throw new ArgumentException("Тренер не найден");
        var specialization = await context.Specializations.FindAsync(entity.Specialization.Id)?? throw new ArgumentException("Специализация не найдена");
        context.Entry(trainer).CurrentValues.SetValues(entity);
        trainer.Specialization = specialization;
        await context.SaveChangesAsync();
    }
    /// <inheritdoc/>
    public async Task<bool> Delete(int id)
    {
        var trainer = await context.Trainers.FindAsync(id);
        if (trainer is null) return false;
        context.Trainers.Remove(trainer);
        await context.SaveChangesAsync();
        return true;
    }
    /// <inheritdoc/>
    public Task<bool> HasTrainersForSpecialization(int specializationId)
    {
        return context.Trainers.AnyAsync(t => t.Specialization.Id == specializationId);
    }
    /// <inheritdoc/>
    public async Task<IReadOnlyList<Trainer>> GetBySpecialization(int specializationId)
    {
        return await context.Trainers.AsNoTracking().Where(t => t.Specialization.Id == specializationId).ToListAsync();
    }
    /// <inheritdoc/>
    public async Task<IReadOnlyList<Trainer>> GetWithMinExperience(int minYears)
    {
        return await context.Trainers.AsNoTracking().Where(t => t.WorkYears >= minYears).ToListAsync();
    }
    /// <inheritdoc/>
    public async Task<IReadOnlyList<Trainer>> GetByIds(IReadOnlyCollection<int> ids)
    {
        return await context.Trainers.AsNoTracking().Where(t => ids.Contains(t.Id)).ToListAsync();
    }
}
