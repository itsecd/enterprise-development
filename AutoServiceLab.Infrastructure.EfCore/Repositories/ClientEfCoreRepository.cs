using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AutoServiceLab.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий клиентов (EF Core)
/// </summary>
public class ClientEfCoreRepository(AutoServiceDbContext context) : IClientRepository
{
    /// <inheritdoc />
    public async Task<Client?> GetAsync(int id) =>
        await context.Clients.FirstOrDefaultAsync(c => c.Id == id);

    /// <inheritdoc />
    public async Task<IReadOnlyList<Client>> GetAllAsync() =>
        await context.Clients.ToListAsync();

    /// <inheritdoc />
    public async Task AddAsync(Client entity)
    {
        await context.Clients.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Client entity)
    {
        context.Clients.Update(entity);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var client = await GetAsync(id);
        if (client == null) return false;

        context.Clients.Remove(client);
        await context.SaveChangesAsync();
        return true;
    }
}
