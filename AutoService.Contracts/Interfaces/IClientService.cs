using AutoService.Contracts.Dtos;

namespace AutoService.Contracts.Interfaces;

/// <summary>
/// Сервис для работы с клиентами.
/// </summary>
public interface IClientService
{
    /// <summary>
    /// Получить список всех клиентов.
    /// </summary>
    public Task<IEnumerable<ClientDto>> GetAllAsync();

    /// <summary>
    /// Получить клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    public Task<ClientDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Добавить нового клиента.
    /// </summary>
    /// <param name="clientDto">Данные нового клиента.</param>
    public Task<ClientDto> CreateAsync(ClientDto clientDto);

    /// <summary>
    /// Обновить данные клиента.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <param name="clientDto">Новые данные клиента.</param>
    public Task UpdateAsync(Guid id, ClientUpdateDto clientDto);

    /// <summary>
    /// Удалить клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    public Task DeleteAsync(Guid id);
}
