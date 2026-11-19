using AutoService.Contracts.Dtos;

namespace AutoService.Contracts.Interfaces;

/// <summary>
/// Сервис для работы с автомобилями.
/// </summary>
public interface ICarService
{
    /// <summary>
    /// Получить список всех автомобилей.
    /// </summary>
    public Task<IEnumerable<CarDto>> GetAllAsync();

    /// <summary>
    /// Получить автомобиль по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    public Task<CarDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Добавить новый автомобиль.
    /// </summary>
    /// <param name="carDto">Данные нового автомобиля.</param>
    public Task<CarDto> CreateAsync(CarDto carDto);

    /// <summary>
    /// Обновить данные автомобиля.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <param name="carDto">Новые данные автомобиля.</param>
    public Task UpdateAsync(Guid id, CarUpdateDto carDto);

    /// <summary>
    /// Удалить автомобиль по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    public Task DeleteAsync(Guid id);
}
