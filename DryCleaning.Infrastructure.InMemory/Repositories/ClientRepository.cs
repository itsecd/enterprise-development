using DryCleaning.Domain;
using DryCleaning.Domain.Interfaces;
using DryCleaning.Domain.Seeds;

namespace DryCleaning.Infrastructure.InMemory.Repositories;

public class ClientRepository(TestData data) : IClientRepository
{
    private readonly List<Client> _clients = [.. data.Clients];
    private int _nextId = 11;

    public Task<IReadOnlyList<Client>> GetAllAsync() => Task.FromResult<IReadOnlyList<Client>>(_clients);
    public Task<Client?> GetByIdAsync(int id) => Task.FromResult(_clients.FirstOrDefault(c => c.ClientId == id));

    public Task<Client> AddAsync(Client client)
    {
        client.ClientId = _nextId++;
        _clients.Add(client);
        return Task.FromResult(client);
    }

    public Task<bool> UpdateAsync(Client client)
    {
        var index = _clients.FindIndex(c => c.ClientId == client.ClientId);
        if (index == -1) return Task.FromResult(false);
        _clients[index] = client;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var c = _clients.FirstOrDefault(x => x.ClientId == id);
        if (c is null) return Task.FromResult(false);
        _clients.Remove(c);
        return Task.FromResult(true);
    }
}
