using FitnessGym.Domain.Entities;

namespace FitnessGym.Domain.Interfaces;

/// <summary>
/// Репозиторий тренеров фитнес-клуба.
/// </summary>
public interface ITrainerRepository : IRepository<Trainer>
{
    /// <summary>
    /// Возвращает тренеров с указанным минимальным стажем.
    /// </summary>
    public Task<IReadOnlyList<Trainer>> GetExperiencedAsync(int minYears);

    /// <summary>
    /// Проверяет, назначена ли специализация тренеру.
    /// </summary>
    public Task<bool> HasSpecializationAsync(int specializationId);
}
