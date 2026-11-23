using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;
using BeautySalon.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core реализация репозитория клиентов.
/// </summary>
public class EfClientRepository(BeautySalonDbContext context) : IClientRepository
{
    /// <inheritdoc />
    public IEnumerable<Client> GetAll() => context.Clients.ToList();

    /// <inheritdoc />
    public Client? GetById(int id) => context.Clients.FirstOrDefault(c => c.Id == id);

    /// <inheritdoc />
    public void Add(Client client)
    {
        context.Clients.Add(client);
        context.SaveChanges();
    }

    /// <inheritdoc />
    public void Update(Client client)
    {
        context.Clients.Update(client);
        context.SaveChanges();
    }

    /// <inheritdoc />
    public void Delete(Client client)
    {
        context.Clients.Remove(client);
        context.SaveChanges();
    }
}