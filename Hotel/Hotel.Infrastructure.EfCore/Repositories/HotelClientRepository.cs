using Hotel.Domain;
using Hotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий клиентов гостиницы
/// </summary>
public class HotelClientRepository(HotelDbContext context) : IRepository<HotelClient, int>
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<HotelClient>> GetAll(CancellationToken cancellationToken = default)
    {
        return await context.HotelClients
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<HotelClient?> GetById(int id, CancellationToken cancellationToken = default)
    {
        return await context.HotelClients.FindAsync([id], cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<HotelClient> Add(HotelClient client, CancellationToken cancellationToken = default)
    {
        context.HotelClients.Add(client);
        await context.SaveChangesAsync(cancellationToken);

        return client;
    }

    /// <inheritdoc/>
    public async Task<HotelClient?> Update(HotelClient client, CancellationToken cancellationToken = default)
    {
        var existingClient = await GetById(client.Id, cancellationToken);

        if (existingClient is null) return null;

        existingClient.PassportNumber = client.PassportNumber;
        existingClient.FirstName = client.FirstName;
        existingClient.LastName = client.LastName;
        existingClient.MiddleName = client.MiddleName;
        existingClient.BirthDate = client.BirthDate;
        existingClient.Citizenship = client.Citizenship;

        await context.SaveChangesAsync(cancellationToken);

        return existingClient;
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
    {
        var client = await GetById(id, cancellationToken);

        if (client is null) return false;

        context.HotelClients.Remove(client);
        await context.SaveDeletionAsync("Нельзя удалить клиента, у которого есть бронирования", cancellationToken);

        return true;
    }
}