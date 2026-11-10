using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Interfaces;
using TransportCompany.Infrastructure.InMemory.Data;

namespace TransportCompany.Infrastructure.InMemory.Repositories;

public class ClientRepository(InMemoryDbContext context) : IClientRepository
{
    public Task<IEnumerable<Client>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(context.Clients.AsEnumerable());
    }

    public Task<Client?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var client = context.Clients.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(client);
    }

    public Task<Client> AddAsync(Client entity, CancellationToken cancellationToken = default)
    {
        entity.Id = context.Clients.Count == 0 ? 0 : context.Clients.Max(c => c.Id) + 1;

        context.Clients.Add(entity);
        return Task.FromResult(entity);
    }

    public Task UpdateAsync(Client entity, CancellationToken cancellationToken = default)
    {
        var existing = context.Clients.FirstOrDefault(c => c.Id == entity.Id);
        if (existing != null)
        {
            existing.Name = entity.Name;
            existing.Phone = entity.Phone;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var existing = context.Clients.FirstOrDefault(c => c.Id == id);
        if (existing != null)
        {
            context.Clients.Remove(existing);
        }
        return Task.CompletedTask;
    }
}