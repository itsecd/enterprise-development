using Hotel.Domain;
using Hotel.Domain.Data;
using Hotel.Domain.Entities;

namespace Hotel.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий клиентов гостиницы в памяти
/// </summary>
public class HotelClientRepository : IRepository<HotelClient, int>
{
    /// <summary>
    /// Данные клиентов гостиницы
    /// </summary>
    private readonly List<HotelClient> _clients;

    /// <summary>
    /// Инициализация репозитория данными
    /// </summary>
    public HotelClientRepository()
    {
        _clients = HotelData.Clients.ToList();
    }

    /// <summary>
    /// Получить всех клиентов
    /// </summary>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<IReadOnlyList<HotelClient>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult<IReadOnlyList<HotelClient>>(_clients.AsReadOnly());
    }

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<HotelClient?> GetById(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(_clients.FirstOrDefault(client => client.Id == id));
    }

    /// <summary>
    /// Добавить нового клиента
    /// </summary>
    /// <param name="client">Новый клиент</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<HotelClient> Add(HotelClient client, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _clients.Add(client);

        return Task.FromResult(client);
    }

    /// <summary>
    /// Обновить существующего клиента
    /// </summary>
    /// <param name="client">Обновлённые данные клиента</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<HotelClient?> Update(HotelClient client, CancellationToken cancellationToken = default)
    {
        var existingClient = await GetById(client.Id, cancellationToken);

        if (existingClient is null) return null;

        existingClient.PassportNumber = client.PassportNumber;
        existingClient.FirstName = client.FirstName;
        existingClient.LastName = client.LastName;
        existingClient.MiddleName = client.MiddleName;
        existingClient.BirthDate = client.BirthDate;
        existingClient.Citizenship = client.Citizenship;

        return existingClient;
    }

    /// <summary>
    /// Удалить клиента по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
    {
        var client = await GetById(id, cancellationToken);

        if (client is null) return false;

        return _clients.Remove(client);
    }
}