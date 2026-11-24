using Hotel.Domain.Models;

namespace Hotel.Domain.Interfaces;

/// <summary>
/// Описывает доступ к клиентам гостиницы
/// </summary>
public interface IClientRepository
{
    /// <summary>
    /// Возвращает всех клиентов
    /// </summary>
    public Task<IReadOnlyList<Client>> GetAllAsync();

    /// <summary>
    /// Возвращает клиента по идентификатору
    /// </summary>
    public Task<Client?> GetByIdAsync(int id);

    /// <summary>
    /// Добавляет клиента и назначает ему идентификатор
    /// </summary>
    public Task<Client> AddAsync(Client client);

    /// <summary>
    /// Обновляет данные клиента
    /// </summary>
    public Task<bool> UpdateAsync(Client client);

    /// <summary>
    /// Удаляет клиента
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}
