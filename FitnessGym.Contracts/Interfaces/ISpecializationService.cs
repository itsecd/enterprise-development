using FitnessGym.Contracts.Dtos;

namespace FitnessGym.Contracts.Interfaces;

/// <summary>
/// Сервис для управления справочником специализаций тренеров
/// </summary>
public interface ISpecializationService
{
    /// <summary>
    /// Получить список всех специализаций
    /// </summary>
    public Task<IReadOnlyList<SpecializationDto>> GetAllAsync();

    /// <summary>
    /// Получить специализацию по её идентификатору
    /// </summary>
    public Task<SpecializationDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создать новую специализацию
    /// </summary>
    public Task<SpecializationDto> CreateAsync(SpecializationDto dto);

    /// <summary>
    /// Удалить специализацию по идентификатору
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}