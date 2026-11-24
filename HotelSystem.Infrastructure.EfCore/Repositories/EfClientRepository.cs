using HotelSystem.Domain.Models;
using HotelSystem.Domain.Repositories;
using HotelSystem.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий клиентов гостиницы с хранением данных в базе PostgreSQL
/// </summary>
public class EfClientRepository(HotelDbContext context) : IRepository<Client>
{
    /// <inheritdoc />
    public async Task<List<Client>> GetAllAsync() =>
        await context.Clients.ToListAsync();

    /// <inheritdoc />
    public async Task<Client?> GetByIdAsync(int id) =>
        await context.Clients.FirstOrDefaultAsync(client => client.Id == id);

    /// <inheritdoc />
    public async Task<Client> AddAsync(Client client)
    {
        await context.Clients.AddAsync(client);
        await context.SaveChangesAsync();

        return client;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, Client client)
    {
        var existingClient = await GetByIdAsync(id);

        if (existingClient is null)
        {
            return false;
        }

        context.Entry(existingClient).CurrentValues.SetValues(client);
        await context.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var client = await GetByIdAsync(id);

        if (client is null)
        {
            return false;
        }

        context.Clients.Remove(client);
        await context.SaveChangesAsync();

        return true;
    }
}