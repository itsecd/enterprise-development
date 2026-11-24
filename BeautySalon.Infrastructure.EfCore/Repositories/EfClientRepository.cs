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
    public async Task<IEnumerable<Client>> GetAllAsync() => await context.Clients.ToListAsync();

    /// <inheritdoc />
    public async Task<Client?> GetByIdAsync(int id) => await context.Clients.FirstOrDefaultAsync(c => c.Id == id);

    /// <inheritdoc />
    public async Task AddAsync(Client entity)
    {
        context.Clients.Add(entity);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Client entity)
    {
        context.Clients.Update(entity);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Client entity)
    {
        context.Clients.Remove(entity);
        await context.SaveChangesAsync();
    }
}