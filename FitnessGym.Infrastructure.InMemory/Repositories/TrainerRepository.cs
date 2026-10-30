using FitnessGym.Domain.Data;
using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Interfaces;

namespace FitnessGym.Infrastructure.InMemory.Repositories;

/// <summary>
/// Реализация репозитория тренеров в памяти
/// </summary>
public class TrainerRepository : ITrainerRepository
{
    private readonly List<Trainer> _trainers = new(DataSeeder.Trainers);

    /// <summary>
    /// Получить список всех тренеров
    /// </summary>
    public Task<IReadOnlyList<Trainer>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyList<Trainer>>(_trainers);
    }

    /// <summary>
    /// Получить тренера по идентификатору
    /// </summary>
    public Task<Trainer?> GetByIdAsync(int id)
    {
        return Task.FromResult(_trainers.FirstOrDefault(t => t.Id == id));
    }

    /// <summary>
    /// Добавить нового тренера
    /// </summary>
    public Task<Trainer> AddAsync(Trainer trainer)
    {
        var newId = _trainers.Count > 0 ? _trainers.Max(t => t.Id) + 1 : 0;
        trainer.Id = newId;
        _trainers.Add(trainer);
        return Task.FromResult(trainer);
    }

    /// <summary>
    /// Обновить данные тренера
    /// </summary>
    public Task<bool> UpdateAsync(Trainer trainer)
    {
        var existing = _trainers.FirstOrDefault(t => t.Id == trainer.Id);
        if (existing is null) return Task.FromResult(false);

        return Task.FromResult(true);
    }

    /// <summary>
    /// Удалить тренера
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        var trainer = _trainers.FirstOrDefault(t => t.Id == id);
        if (trainer is null) return Task.FromResult(false);

        _trainers.Remove(trainer);
        return Task.FromResult(true);
    }
}