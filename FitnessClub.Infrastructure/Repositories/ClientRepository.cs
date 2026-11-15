using FitnessClub.Domain.Data;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Infrastructure.Repositories;

/// <summary>
/// In-memory репозиторий клиентов
/// </summary>
public class ClientRepository: IClientRepository
{
    /// <inheritdoc/>
    public Task<Client?> Get(int id)
    {
        return Task.FromResult(FitnessClubData.Clients.FirstOrDefault(client => client.Id == id));
    }
    /// <inheritdoc/>
    public Task<IReadOnlyList<Client>> GetAll()
    {
        return Task.FromResult<IReadOnlyList<Client>>(FitnessClubData.Clients);
    }
    /// <inheritdoc/>
    public Task Add(Client entity)
    {
        var clients = FitnessClubData.Clients;
        entity.Id = clients.Count == 0?1:clients.Max(x => x.Id) + 1;
        clients.Add(entity);
        return Task.CompletedTask;
    }
    /// <inheritdoc/>
    public Task Update(Client entity)
    {
        var clients = FitnessClubData.Clients;
        var index = clients.FindIndex(client => client.Id == entity.Id);
        if (index == -1)
        {
            throw new KeyNotFoundException($"Клиент с Id: {entity.Id} не найден");
        }
        clients[index] = entity;
        return Task.CompletedTask;
    }
    /// <inheritdoc/>
    public async Task<bool> Delete(int id)
    {
        var client = await Get(id);
        if (client is null)
        {
            return false;
        }
        return FitnessClubData.Clients.Remove(client);
    }
    /// <inheritdoc/>
    public async Task<IReadOnlyList<Client>> GetExpiredSubscriptions(DateOnly today)
    {
        return (await GetAll())
            .Where(client => client.EndSub < today)
            .OrderBy(client => client.LastName)
            .ThenBy(client => client.FirstName)
            .ThenBy(client => client.Patronymic)
            .ToList();
    }
}
