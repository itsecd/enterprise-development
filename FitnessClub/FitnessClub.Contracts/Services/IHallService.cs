using FitnessClub.Contracts.Dtos;

namespace FitnessClub.Contracts.Services;

/// <summary>
/// Определяет операции для работы с залами фитнес-клуба.
/// </summary>
public interface IHallService
{
    /// <summary>Возвращает все залы.</summary>
    public Task<IReadOnlyCollection<HallDto>> GetAllAsync();

    /// <summary>Возвращает зал по ID.</summary>
    /// <param name="id">ID зала.</param>
    public Task<HallDto?> GetByIdAsync(int id);

    /// <summary>Создаёт зал.</summary>
    /// <param name="dto">Данные нового зала.</param>
    public Task<HallDto> CreateAsync(CreateHallDto dto);

    /// <summary>Обновляет зал.</summary>
    /// <param name="id">ID зала.</param>
    /// <param name="dto">Новые данные зала.</param>
    public Task<bool> UpdateAsync(int id, UpdateHallDto dto);

    /// <summary>Удаляет зал.</summary>
    /// <param name="id">ID зала.</param>
    public Task<bool> DeleteAsync(int id);
}