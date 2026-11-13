using BeautySalon.Domain;
using BeautySalon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий для клиентов с Entity Framework Core
/// </summary>
/// <param name="context">Контекст БД</param>
public class ClientEfCoreRepository(BeautySalonDbContext context) : IRepository<Client, int>
{
    /// <inheritdoc/>
    public async Task<Client> Create(Client entity)
    {
        var result = await context.Clients.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int entityId)
    {
        var result = await context.Clients.FirstOrDefaultAsync(c => c.Id == entityId);
        if (result == null)
            return false;

        context.Clients.Remove(result);
        await context.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc/>
    public async Task<Client?> Read(int entityId) => await context.Clients.FirstOrDefaultAsync(c => c.Id == entityId);

    /// <inheritdoc/>
    public async Task<List<Client>> ReadAll() => await context.Clients.ToListAsync();

    /// <inheritdoc/>
    public async Task<Client> Update(Client entity)
    {
        context.Clients.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id))!;
    }
}
