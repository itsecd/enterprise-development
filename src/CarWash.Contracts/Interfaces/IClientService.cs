using CarWash.Contracts.Dto;

namespace CarWash.Contracts.Interfaces;

/// <summary>
/// Сервис для работы с клиентами и их автомобилями
/// </summary>
public interface IClientService
{
    /// <summary>
    /// Возвращает всех клиентов
    /// </summary>
    /// <returns>Список клиентов</returns>
    public Task<IEnumerable<ClientDto>> GetAllAsync();

    /// <summary>
    /// Возвращает клиента по ID
    /// </summary>
    /// <param name="id">ID клиента</param>
    /// <returns>Данные клиента или null, если не найдено</returns>
    public Task<ClientDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создаёт клиента
    /// </summary>
    /// <param name="editDto">Данные клиента</param>
    /// <returns>Созданный объект</returns>
    public Task<ClientDto> CreateAsync(ClientEditDto editDto);

    /// <summary>
    /// Изменяет клиента
    /// </summary>
    /// <param name="id">ID клиента</param>
    /// <param name="editDto">Новые данные клиента</param>
    /// <exception cref="KeyNotFoundException">Объект не найден</exception>
    public Task UpdateAsync(int id, ClientEditDto editDto);

    /// <summary>
    /// Удаляет клиента
    /// </summary>
    /// <param name="id">ID клиента</param>
    /// <exception cref="KeyNotFoundException">Объект не найден</exception>
    public Task DeleteAsync(int id);

    /// <summary>
    /// Возвращает автомобили клиента
    /// </summary>
    /// <param name="clientId">ID клиента</param>
    /// <returns>Список автомобилей или null, если клиент не найден</returns>
    public Task<IEnumerable<CarDto>?> GetCarsAsync(int clientId);

    /// <summary>
    /// Привязывает автомобиль к клиенту
    /// </summary>
    /// <param name="clientId">ID клиента</param>
    /// <param name="carId">ID автомобиля</param>
    /// <exception cref="KeyNotFoundException">Клиент или автомобиль не найден</exception>
    /// <exception cref="InvalidOperationException">Автомобиль уже принадлежит другому клиенту</exception>
    public Task AddCarAsync(int clientId, int carId);

    /// <summary>
    /// Отвязывает автомобиль от клиента
    /// </summary>
    /// <param name="clientId">ID клиента</param>
    /// <param name="carId">ID автомобиля</param>
    /// <exception cref="KeyNotFoundException">Клиент не найден или автомобиль ему не принадлежит</exception>
    public Task RemoveCarAsync(int clientId, int carId);
}
