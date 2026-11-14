using CarWash.Contracts.Dto;

namespace CarWash.Contracts.Interfaces;

/// <summary>
/// Сервис для работы с услугами
/// </summary>
public interface IServiceService
{
    /// <summary>
    /// Возвращает все услуги
    /// </summary>
    /// <returns>Список услуг</returns>
    public Task<IEnumerable<ServiceDto>> GetAllAsync();

    /// <summary>
    /// Возвращает услугу по ID
    /// </summary>
    /// <param name="id">ID услуги</param>
    /// <returns>Данные услуги или null, если не найдено</returns>
    public Task<ServiceDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создаёт услугу
    /// </summary>
    /// <param name="editDto">Данные услуги</param>
    /// <returns>Созданный объект</returns>
    public Task<ServiceDto> CreateAsync(ServiceEditDto editDto);

    /// <summary>
    /// Изменяет услугу
    /// </summary>
    /// <param name="id">ID услуги</param>
    /// <param name="editDto">Новые данные услуги</param>
    /// <exception cref="KeyNotFoundException">Объект не найден</exception>
    public Task UpdateAsync(int id, ServiceEditDto editDto);

    /// <summary>
    /// Удаляет услугу
    /// </summary>
    /// <param name="id">ID услуги</param>
    /// <exception cref="KeyNotFoundException">Объект не найден</exception>
    /// <exception cref="InvalidOperationException">Услуга используется в заказах</exception>
    public Task DeleteAsync(int id);
}
