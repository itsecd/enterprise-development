using FitnessGym.Domain.Entities;

namespace FitnessGym.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы со специализациями
/// </summary>
public interface ISpecializationRepository
{
    /// <summary>
    /// Получить список всех специализаций
    /// </summary>
    public Task<IReadOnlyList<Specialization>> GetAllAsync();

    /// <summary>
    /// Получить специализацию по идентификатору
    /// </summary>
    public Task<Specialization?> GetByIdAsync(int id);

    /// <summary>
    /// Добавить новую специализацию
    /// </summary>
    public Task<Specialization> AddAsync(Specialization specialization);

    /// <summary>
    /// Обновить данные специализации
    /// </summary>
    public Task<bool> UpdateAsync(Specialization specialization);

    /// <summary>
    /// Удалить специализацию
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}