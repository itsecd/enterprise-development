using FitnessClub.Domain.Entities;

namespace FitnessClub.Domain.Repositories;

/// <summary>
/// Контракт репозитория тренеров
/// </summary>
public interface ITrainerRepository : IRepository<Trainer, int>
{
    /// <summary>
    /// Проверяет наличие тренеров выбранной специализации
    /// </summary>
    Task<bool> HasTrainersForSpecialization(int specializationId);
    /// <summary>
    /// Получает тренеров выбранной специализации
    /// </summary>
    Task<IReadOnlyList<Trainer>> GetBySpecialization(int specializationId);
    /// <summary>
    /// Получает тренеров с указанным минимальным стажем
    /// </summary>
    Task<IReadOnlyList<Trainer>> GetWithMinExperience(int minYears);
    /// <summary>
    /// Получает тренеров по списку идентификаторов
    /// </summary>
    Task<IReadOnlyList<Trainer>> GetByIds(IReadOnlyCollection<int> ids);
}
