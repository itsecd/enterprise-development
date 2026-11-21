using Microsoft.EntityFrameworkCore;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Interfaces;
using TransportCompany.Infrastructure.MsSqlServer.Data;

namespace TransportCompany.Infrastructure.MsSqlServer.Repositories;

/// <summary>
/// Репозиторий клиентов
/// </summary>
public class ClientRepository(TransportCompanyDbContext context) : IClientRepository
{
    public async Task<IEnumerable<Client>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Clients
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Client?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Client> AddAsync(Client entity, CancellationToken cancellationToken = default)
    {
        await context.Clients.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Client?> UpdateAsync(Client entity, CancellationToken cancellationToken = default)
    {
        var existing = await context.Clients.FirstOrDefaultAsync(c => c.Id == entity.Id, cancellationToken);
        if (existing == null)
        {
            return null;
        }

        existing.Name = entity.Name;
        existing.Phone = entity.Phone;

        await context.SaveChangesAsync(cancellationToken);
        return existing;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var existing = await context.Clients.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        if (existing == null)
        {
            return false;
        }

        context.Clients.Remove(existing);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}