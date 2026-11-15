using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Contracts.Interfaces;

/// <summary>
/// Сервис для управления выставками
/// </summary>
public interface IExhibitionService
{
    /// <summary>
    /// Получить список всех выставок
    /// </summary>
    public Task<IEnumerable<ExhibitionResponse>> GetAllAsync();

    /// <summary>
    /// Получить выставку по её идентификатору
    /// </summary>
    public Task<ExhibitionResponse?> GetByIdAsync(int id);

    /// <summary>
    /// Создать новую выставку
    /// </summary>
    public Task<ExhibitionResponse> CreateAsync(ExhibitionCreateRequest request);

    /// <summary>
    /// Обновить данные существующей выставки
    /// </summary>
    public Task<bool> UpdateAsync(int id, ExhibitionUpdateRequest request);

    /// <summary>
    /// Удалить выставку
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}