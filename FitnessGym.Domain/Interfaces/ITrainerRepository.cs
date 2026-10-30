using FitnessGym.Domain.Entities;

namespace FitnessGym.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с тренерами
/// </summary>
public interface ITrainerRepository
{
    public Task<IReadOnlyList<Trainer>> GetAllAsync();
    public Task<Trainer?> GetByIdAsync(int id);
    public Task<Trainer> AddAsync(Trainer trainer);
    public Task<bool> UpdateAsync(Trainer trainer);
    public Task<bool> DeleteAsync(int id);
}