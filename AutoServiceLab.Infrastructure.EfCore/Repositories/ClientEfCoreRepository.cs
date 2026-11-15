using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;

namespace AutoServiceLab.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий клиентов (EF Core)
/// </summary>
public class ClientEfCoreRepository(AutoServiceDbContext context) : IClientRepository
{
    /// <inheritdoc />
    public Client? Get(int id) => context.Clients.FirstOrDefault(c => c.Id == id);

    /// <inheritdoc />
    public IReadOnlyList<Client> GetAll() => context.Clients.ToList();

    /// <inheritdoc />
    public void Add(Client entity)
    {
        context.Clients.Add(entity);
        context.SaveChanges();
    }

    /// <inheritdoc />
    public void Update(Client entity)
    {
        context.Clients.Update(entity);
        context.SaveChanges();
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var client = Get(id);
        if (client == null) return false;

        context.Clients.Remove(client);
        context.SaveChanges();
        return true;
    }
}
