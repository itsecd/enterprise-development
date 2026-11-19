using AutoService.Domain.Interfaces;
using AutoService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Infrastructure.Persistence.Repositories;

/// <summary>
/// Реализация репозитория клиентов на Entity Framework Core.
/// </summary>
public class EfClientRepository(AutoServiceDbContext context) : IClientRepository
{
    /// <summary>
    /// Получить список всех клиентов.
    /// </summary>
    public async Task<IEnumerable<Client>> GetAllAsync() => await context.Clients.ToListAsync();

    /// <summary>
    /// Получить клиента по его идентификатору.
    /// </summary>
    public async Task<Client?> GetByIdAsync(Guid id) => await context.Clients.FirstOrDefaultAsync(c => c.Id == id);

    /// <summary>
    /// Добавить нового клиента.
    /// </summary>
    public async Task AddAsync(Client client)
    {
        context.Clients.Add(client);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновить данные существующего клиента.
    /// </summary>
    public async Task UpdateAsync(Client client)
    {
        var existing = await context.Clients.FindAsync(client.Id);
        if (existing is null) return;

        existing.FullName = client.FullName;
        existing.Phone = client.Phone;

        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Удалить клиента.
    /// </summary>
    public async Task DeleteAsync(Client client)
    {
        context.Clients.Remove(client);
        await context.SaveChangesAsync();
    }
}
