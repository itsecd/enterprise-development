using Hotel.Domain.Data;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Models;

namespace Hotel.Infrastructure.InMemory.Repositories;

/// <summary>Репозиторий клиентов с хранением данных в памяти.</summary>
/// <param name="data">Общий набор данных гостиницы.</param>
public class ClientRepository(TestData data) : IClientRepository
{
    private readonly TestData _data = data;

    public Task<IReadOnlyList<Client>> GetAllAsync()
    {
        lock (_data.Clients)
            return Task.FromResult<IReadOnlyList<Client>>(_data.Clients.ToList());
    }

    public Task<Client?> GetByIdAsync(int id)
    {
        lock (_data.Clients)
            return Task.FromResult(_data.Clients.FirstOrDefault(client => client.Id == id));
    }

    public Task<Client> AddAsync(Client client)
    {
        lock (_data.Clients)
        {
            client.Id = _data.Clients.Select(item => item.Id).DefaultIfEmpty(0).Max() + 1;
            _data.Clients.Add(client);
        }

        return Task.FromResult(client);
    }

    public Task<bool> UpdateAsync(Client client)
    {
        lock (_data.Clients)
        {
            var existing = _data.Clients.FirstOrDefault(item => item.Id == client.Id);
            if (existing is null)
                return Task.FromResult(false);

            existing.PassportNumber = client.PassportNumber;
            existing.FullName = client.FullName;
            existing.BirthDate = client.BirthDate;
            existing.Citizenship = client.Citizenship;
            return Task.FromResult(true);
        }
    }

    public Task<bool> DeleteAsync(int id)
    {
        lock (_data.Clients)
        {
            var client = _data.Clients.FirstOrDefault(item => item.Id == id);
            return Task.FromResult(client is not null && _data.Clients.Remove(client));
        }
    }
}
