using CarWash.Contracts.Dto;

namespace CarWash.Contracts.Interfaces;

/// <summary>
/// Сервис для работы с клиентами
/// </summary>
public interface IClientService
{
    public Task<IEnumerable<ClientDto>> GetAllAsync();

    public Task<ClientDto?> GetByIdAsync(int id);

    public Task<ClientDto> CreateAsync(ClientEditDto editDto);

    public Task UpdateAsync(int id, ClientEditDto editDto);

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
