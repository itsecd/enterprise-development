using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.Ef.Repositories;

/// <summary>
/// Репозиторий персональных тренировок с хранением в БД
/// </summary>
public class PersonalTrainingSessionEfRepository(FitnessClubDbContext context): IPersonalTrainingSessionRepository
{
    /// <inheritdoc/>
    public async Task<PersonalTrainingSession?> Get(int id)
    {
        return await context.PersonalTrainingSessions.FirstOrDefaultAsync(session => session.Id == id);
    }
    /// <inheritdoc/>
    public async Task<IReadOnlyList<PersonalTrainingSession>> GetAll()
    {
        return await context.PersonalTrainingSessions.ToListAsync();
    }
    /// <inheritdoc/>
    public async Task Add(PersonalTrainingSession entity)
    {
        var client = await context.Clients.FindAsync(entity.Client.Id)?? throw new ArgumentException("Клиент не найден");
        var trainer = await context.Trainers.FindAsync(entity.Trainer.Id)?? throw new ArgumentException("Тренер не найден");
        entity.Client = client;
        entity.Trainer = trainer;
        context.PersonalTrainingSessions.Add(entity);
        await context.SaveChangesAsync();
    }
    /// <inheritdoc/>
    public async Task Update(PersonalTrainingSession entity)
    {
        var session = await context.PersonalTrainingSessions.FirstOrDefaultAsync(s => s.Id == entity.Id)?? throw new ArgumentException("Тренировка не найдена");
        var client = await context.Clients.FindAsync(entity.Client.Id)?? throw new ArgumentException("Клиент не найден");
        var trainer = await context.Trainers.FindAsync(entity.Trainer.Id)?? throw new ArgumentException("Тренер не найден");
        context.Entry(session).CurrentValues.SetValues(entity);
        session.Client = client;
        session.Trainer = trainer;
        await context.SaveChangesAsync();
    }
    /// <inheritdoc/>
    public async Task<bool> Delete(int id)
    {
        var session = await context.PersonalTrainingSessions.FindAsync(id);
        if (session is null) return false;
        context.PersonalTrainingSessions.Remove(session);
        await context.SaveChangesAsync();
        return true;
    }
    /// <inheritdoc/>
    public Task<bool> HasSessionsForClient(int clientId)
    {
        return context.PersonalTrainingSessions.AnyAsync(s => s.Client.Id == clientId);
    }
    /// <inheritdoc/>
    public Task<bool> HasSessionsForTrainer(int trainerId)
    {
        return context.PersonalTrainingSessions.AnyAsync(s => s.Trainer.Id == trainerId);
    }
    /// <inheritdoc/>
    public async Task<IReadOnlyList<PersonalTrainingSession>> GetByClient(int clientId)
    {
        return await context.PersonalTrainingSessions.AsNoTracking().Where(s => s.Client.Id == clientId).ToListAsync();
    }
    /// <inheritdoc/>
    public async Task<IReadOnlyList<PersonalTrainingSession>> GetByTrainer(int trainerId)
    {
        return await context.PersonalTrainingSessions.AsNoTracking().Where(s => s.Trainer.Id == trainerId).ToListAsync();
    }
    /// <inheritdoc/>
    public Task<bool> HallExists(string hallName)
    {
        return context.PersonalTrainingSessions.AnyAsync(s => s.HallName == hallName);
    }
    /// <inheritdoc/>
    public Task<bool> HasSessionInHall(string hallName, DateTime checkTime)
    {
        return context.PersonalTrainingSessions.AnyAsync(s => s.HallName == hallName && s.TrainDay == checkTime);
    }
    /// <inheritdoc/>
    public async Task<IReadOnlyList<PersonalTrainingSession>> GetByHallAndPeriod(string hallName, DateTime start, DateTime end)
    {
        return await context.PersonalTrainingSessions.AsNoTracking()
            .Where(s => s.HallName == hallName && s.TrainDay >= start && s.TrainDay < end)
            .ToListAsync();
    }
    /// <inheritdoc/>
    public async Task<IReadOnlyList<int>> GetTopFiveTrainerIds()
    {
        return await context.PersonalTrainingSessions
            .GroupBy(s => s.Trainer.Id)
            .Select(g => new { TrainerId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.TrainerId)
            .Take(5)
            .Select(x => x.TrainerId)
            .ToListAsync();
    }
}
