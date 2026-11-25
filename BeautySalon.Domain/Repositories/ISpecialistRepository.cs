using BeautySalon.Domain.Entities;

namespace BeautySalon.Domain.Repositories;

/// <summary>
/// Хранение специалистов
/// </summary>
public interface ISpecialistRepository : IRepository<BeautySpecialist>
{
    /// <summary>
    /// Получить специалистов со стажем от указанного числа лет
    /// </summary>
    Task<IReadOnlyList<BeautySpecialist>> GetByMinimumExperienceAsync(int years, CancellationToken cancellationToken = default);
}
