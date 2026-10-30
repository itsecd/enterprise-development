using FitnessGym.Domain.Data;
using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Interfaces;

namespace FitnessGym.Infrastructure.InMemory.Repositories;

/// <summary>
/// Реализация репозитория занятий в памяти
/// </summary>
public class TrainingSessionRepository : ITrainingSessionRepository
{
    private readonly List<TrainingSession> _sessions = new(DataSeeder.Sessions);

    /// <summary>
    /// Получить список всех занятий
    /// </summary>
    public Task<IReadOnlyList<TrainingSession>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyList<TrainingSession>>(_sessions);
    }

    /// <summary>
    /// Получить занятие по идентификатору
    /// </summary>
    public Task<TrainingSession?> GetByIdAsync(int id)
    {
        return Task.FromResult(_sessions.FirstOrDefault(s => s.Id == id));
    }

    /// <summary>
    /// Добавить новую запись на занятие
    /// </summary>
    public Task<TrainingSession> AddAsync(TrainingSession session)
    {
        var newId = _sessions.Count > 0 ? _sessions.Max(s => s.Id) + 1 : 0;
        session.Id = newId;
        _sessions.Add(session);
        return Task.FromResult(session);
    }

    /// <summary>
    /// Удалить запись о занятии
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        var session = _sessions.FirstOrDefault(s => s.Id == id);
        if (session is null) return Task.FromResult(false);

        _sessions.Remove(session);
        return Task.FromResult(true);
    }
}