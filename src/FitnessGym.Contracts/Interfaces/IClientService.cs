using FitnessGym.Contracts.Dto;

namespace FitnessGym.Contracts.Interfaces;

/// <summary>
/// Сервис клиентов фитнес-клуба.
/// </summary>
public interface IClientService
{
    /// <summary>
    /// Возвращает список всех клиентов.
    /// </summary>
    public Task<IReadOnlyList<ClientDto>> GetClientsAsync();

    /// <summary>
    /// Возвращает клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    public Task<ClientDto?> GetClientAsync(int id);

    /// <summary>
    /// Добавляет клиента и возвращает созданную сущность с присвоенным идентификатором.
    /// </summary>
    /// <param name="client">Данные клиента.</param>
    public Task<ClientDto> AddClientAsync(UpsertClientDto client);

    /// <summary>
    /// Заменяет клиента с указанным идентификатором.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <param name="client">Новые данные клиента.</param>
    /// <returns>Обновлённый клиент или null, если он не найден.</returns>
    public Task<ClientDto?> UpdateClientAsync(int id, UpsertClientDto client);

    /// <summary>
    /// Удаляет клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <returns>true, если клиент найден и удалён.</returns>
    public Task<bool> DeleteClientAsync(int id);

    /// <summary>
    /// Возвращает клиентов с просроченным абонементом, упорядоченных по ФИО.
    /// </summary>
    public Task<IReadOnlyList<ClientDto>> GetClientsWithExpiredSubscriptionAsync();
}
