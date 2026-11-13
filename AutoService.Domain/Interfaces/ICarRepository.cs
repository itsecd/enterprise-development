using AutoService.Domain.Models;

namespace AutoService.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с автомобилями.
/// </summary>
public interface ICarRepository
{
    /// <summary>
    /// Получить список всех автомобилей.
    /// </summary>
    public Task<IEnumerable<Car>> GetAllAsync();

    /// <summary>
    /// Получить автомобиль по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    public Task<Car?> GetByIdAsync(Guid id);

    /// <summary>
    /// Добавить новый автомобиль.
    /// </summary>
    /// <param name="car">Объект автомобиля.</param>
    public Task AddAsync(Car car);

    /// <summary>
    /// Обновить данные существующего автомобиля.
    /// </summary>
    /// <param name="car">Объект автомобиля с обновленными данными.</param>
    public Task UpdateAsync(Car car);

    /// <summary>
    /// Удалить автомобиль.
    /// </summary>
    /// <param name="car">Объект автомобиля для удаления.</param>
    public Task DeleteAsync(Car car);
}