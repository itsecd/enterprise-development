using TransportCompany.Contracts.Dto;

namespace TransportCompany.Contracts.Interfaces;

/// <summary>
/// Сервис для работы с клиентами транспортной компании
/// </summary>
public interface IClientService
{
    /// <summary>
    /// Получить список всех клиентов
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список клиентов</returns>
    public Task<IEnumerable<ClientDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Клиент или null, если не найден</returns>
    public Task<ClientDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создать нового клиента
    /// </summary>
    /// <param name="dto">Данные клиента</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданный клиент</returns>
    public Task<ClientDto> CreateAsync(ClientEditDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить данные клиента
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <param name="dto">Новые данные клиента</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<ClientDto?> UpdateAsync(int id, ClientEditDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить клиента
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}