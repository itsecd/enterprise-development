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
    public Task<IReadOnlyList<HotelClient>> GetAll()
    {
        return Task.FromResult<IReadOnlyList<HotelClient>>(_clients.AsReadOnly());
    }

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    public Task<HotelClient?> GetById(int id)
    {
        return Task.FromResult(_clients.FirstOrDefault(client => client.Id == id));
    }

    /// <summary>
    /// Добавить нового клиента
    /// </summary>
    public Task<HotelClient> Add(HotelClient client)
    {
        _clients.Add(client);

        return Task.FromResult(client);
    }

    /// <summary>
    /// Обновить существующего клиента
    /// </summary>
    public async Task<HotelClient?> Update(HotelClient client)
    {
        var existingClient = await GetById(client.Id);

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
    public async Task<bool> Delete(int id)
    {
        var client = await GetById(id);

        if (client is null) return false;

        return _clients.Remove(client);
    }
}