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
    public async Task<IReadOnlyList<HotelClient>> GetAll()
    {
        return await context.HotelClients
            .AsNoTracking()
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<HotelClient?> GetById(int id)
    {
        return await context.HotelClients.FindAsync(id);
    }
 
    /// <inheritdoc/>
    public async Task<HotelClient> Add(HotelClient client)
    {
        context.HotelClients.Add(client);
        await context.SaveChangesAsync();

        return client;
    }

    /// <inheritdoc/>
    public async Task<HotelClient?> Update(HotelClient client)
    {
        var existingClient = await GetById(client.Id);

        if (existingClient is null) return null;

        existingClient.PassportNumber = client.PassportNumber;
        existingClient.FirstName = client.FirstName;
        existingClient.LastName = client.LastName;
        existingClient.MiddleName = client.MiddleName;
        existingClient.BirthDate = client.BirthDate;
        existingClient.Citizenship = client.Citizenship;

        await context.SaveChangesAsync();

        return existingClient;
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int id)
    {
        var client = await GetById(id);

        if (client is null) return false;

        context.HotelClients.Remove(client);
        await context.SaveChangesAsync();

        return true;
    }
}