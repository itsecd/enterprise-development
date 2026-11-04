using BeautySalon.Domain.Entities;

namespace BeautySalon.Domain.Repositories;

/// <summary>
/// Репозиторий для работы с клиентами.
/// </summary>
public interface IClientRepository
{
    /// <summary>
    /// Получить всех клиентов.
    /// </summary>
    public IEnumerable<Client> GetAll();
    
    /// <summary>
    /// Получить клиента по ID.
    /// </summary>
    public Client? GetById(int id);
    
    /// <summary>
    /// Добавить клиента.
    /// </summary>
    public void Add(Client client);
    
    /// <summary>
    /// Обновить данные клиента.
    /// </summary>
    public void Update(Client client);
    
    /// <summary>
    /// Удалить клиента.
    /// </summary>
    public void Delete(Client client);
}