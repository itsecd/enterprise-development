using FitnessClub.Contracts.Dtos;

namespace FitnessClub.Contracts.Services;

/// <summary>
/// Определяет операции для работы со специализациями тренеров.
/// </summary>
public interface ISpecializationService
{
    /// <summary>Возвращает все специализации.</summary>
    public Task<IReadOnlyCollection<SpecializationDto>> GetAllAsync();

    /// <summary>Возвращает специализацию по ID.</summary>
    /// <param name="id">ID специализации.</param>
    public Task<SpecializationDto?> GetByIdAsync(int id);

    /// <summary>Создаёт специализацию.</summary>
    /// <param name="dto">Данные новой специализации.</param>
    public Task<SpecializationDto> CreateAsync(CreateSpecializationDto dto);

    /// <summary>Обновляет специализацию.</summary>
    /// <param name="id">ID специализации.</param>
    /// <param name="dto">Новые данные специализации.</param>
    public Task<bool> UpdateAsync(int id, UpdateSpecializationDto dto);

    /// <summary>Удаляет специализацию.</summary>
    /// <param name="id">ID специализации.</param>
    public Task<bool> DeleteAsync(int id);
}