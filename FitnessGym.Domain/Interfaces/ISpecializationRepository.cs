using FitnessGym.Domain.Entities;

namespace FitnessGym.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы со специализациями
/// </summary>
public interface ISpecializationRepository
{
    public Task<IReadOnlyList<Specialization>> GetAllAsync();
    public Task<Specialization?> GetByIdAsync(int id);
    public Task<Specialization> AddAsync(Specialization specialization);
    public Task<bool> DeleteAsync(int id);
}