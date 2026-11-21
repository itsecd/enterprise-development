using CarWash.Contracts.Dto;

namespace CarWash.Contracts.Interfaces;

/// <summary>
/// Сервис для работы с автомобилями
/// </summary>
public interface ICarService
{
    /// <summary>
    /// Возвращает все автомобили
    /// </summary>
    /// <returns>Список автомобилей</returns>
    public Task<IEnumerable<CarDto>> GetAllAsync();

    /// <summary>
    /// Возвращает автомобиль по ID
    /// </summary>
    /// <param name="id">ID автомобиля</param>
    /// <returns>Данные автомобиля или null, если не найдено</returns>
    public Task<CarDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создаёт автомобиль
    /// </summary>
    /// <param name="editDto">Данные автомобиля</param>
    /// <returns>Созданный объект</returns>
    public Task<CarDto> CreateAsync(CarEditDto editDto);

    /// <summary>
    /// Изменяет автомобиль
    /// </summary>
    /// <param name="id">ID автомобиля</param>
    /// <param name="editDto">Новые данные автомобиля</param>
    /// <exception cref="KeyNotFoundException">Объект не найден</exception>
    public Task UpdateAsync(int id, CarEditDto editDto);

    /// <summary>
    /// Удаляет автомобиль
    /// </summary>
    /// <param name="id">ID автомобиля</param>
    /// <exception cref="KeyNotFoundException">Объект не найден</exception>
    /// <exception cref="InvalidOperationException">Автомобиль используется в заказах</exception>
    public Task DeleteAsync(int id);
}
