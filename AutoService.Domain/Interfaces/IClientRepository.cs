using AutoService.Domain.Models;

namespace AutoService.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с клиентами.
/// </summary>
public interface IClientRepository
{
    /// <summary>
    /// Получить список всех клиентов.
    /// </summary>
    public Task<IEnumerable<Client>> GetAllAsync();

    /// <summary>
    /// Получить клиента по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    public Task<Client?> GetByIdAsync(Guid id);

    /// <summary>
    /// Добавить нового клиента.
    /// </summary>
    /// <param name="client">Объект клиента.</param>
    public Task AddAsync(Client client);

    /// <summary>
    /// Обновить данные существующего клиента.
    /// </summary>
    /// <param name="client">Объект клиента с обновленными данными.</param>
    public Task UpdateAsync(Client client);

    /// <summary>
    /// Удалить клиента.
    /// </summary>
    /// <param name="client">Объект клиента для удаления.</param>
    public Task DeleteAsync(Client client);
}