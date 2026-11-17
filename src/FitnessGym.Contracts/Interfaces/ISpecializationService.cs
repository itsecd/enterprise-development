using FitnessGym.Contracts.Dto;

namespace FitnessGym.Contracts.Interfaces;

/// <summary>
/// Сервис специализаций фитнес-клуба.
/// </summary>
public interface ISpecializationService
{
    /// <summary>
    /// Возвращает список всех специализаций (справочник).
    /// </summary>
    public Task<IReadOnlyList<SpecializationDto>> GetSpecializationsAsync();

    /// <summary>
    /// Возвращает специализацию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор специализации.</param>
    public Task<SpecializationDto?> GetSpecializationAsync(int id);

    /// <summary>
    /// Добавляет специализацию и возвращает созданную сущность с присвоенным идентификатором.
    /// </summary>
    /// <param name="specialization">Данные специализации.</param>
    public Task<SpecializationDto> AddSpecializationAsync(UpsertSpecializationDto specialization);

    /// <summary>
    /// Заменяет специализацию с указанным идентификатором.
    /// </summary>
    /// <param name="id">Идентификатор специализации.</param>
    /// <param name="specialization">Новые данные специализации.</param>
    /// <returns>Обновлённая специализация или null, если она не найдена.</returns>
    public Task<SpecializationDto?> UpdateSpecializationAsync(int id, UpsertSpecializationDto specialization);

    /// <summary>
    /// Удаляет специализацию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор специализации.</param>
    /// <returns>true, если специализация найдена и удалена.</returns>
    public Task<bool> DeleteSpecializationAsync(int id);
}
