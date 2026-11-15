using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.Ef.Repositories;

/// <summary>
/// Репозиторий клиентов с хранением в БД
/// </summary>
public class ClientEfRepository(FitnessClubDbContext context): IClientRepository
{
    /// <inheritdoc/>
    public async Task<Client?> Get(int id)
    {
        return await context.Clients.FindAsync(id);
    }
    /// <inheritdoc/>
    public async Task<IReadOnlyList<Client>> GetAll()
    {
        return await context.Clients.ToListAsync();
    }
    /// <inheritdoc/>
    public async Task Add(Client entity)
    {
        context.Clients.Add(entity);
        await context.SaveChangesAsync();
    }
    /// <inheritdoc/>
    public async Task Update(Client entity)
    {
        context.Clients.Update(entity);
        await context.SaveChangesAsync();
    }
    /// <inheritdoc/>
    public async Task<bool> Delete(int id)
    {
        var client = await context.Clients.FindAsync(id);
        if (client is null) return false;
        context.Clients.Remove(client);
        await context.SaveChangesAsync();
        return true;
    }
    /// <inheritdoc/>
    public async Task<IReadOnlyList<Client>> GetExpiredSubscriptions(DateOnly today)
    {
        return await context.Clients
            .AsNoTracking()
            .Where(client => client.EndSub < today)
            .OrderBy(client => client.LastName)
            .ThenBy(client => client.FirstName)
            .ThenBy(client => client.Patronymic)
            .ToListAsync();
}
}
