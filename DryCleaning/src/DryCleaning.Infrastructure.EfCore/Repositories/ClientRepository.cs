using DryCleaning.Domain;
using DryCleaning.Domain.Interfaces;
using DryCleaning.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace DryCleaning.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий клиентов в базе данных
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class ClientRepository(DryCleaningDbContext context) : IClientRepository
{
    /// <summary>
    /// Загружает всех клиентов из базы
    /// </summary>
    /// <returns>Список клиентов</returns>
    public async Task<IReadOnlyList<Client>> GetAllAsync()
    {
        return await context.Clients.ToListAsync();
    }

    /// <summary>
    /// Ищет клиента по первичному ключу.
    /// Если клиент уже загружен в контекст, запрос к базе не выполняется
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>Клиент или null, если он не найден</returns>
    public async Task<Client?> GetByIdAsync(int id)
    {
        return await context.Clients.FindAsync(id);
    }

    /// <summary>
    /// Добавляет клиента в базу.
    /// Идентификатор назначает база при сохранении
    /// </summary>
    /// <param name="client">Новый клиент</param>
    /// <returns>Клиент с назначенным идентификатором</returns>
    public async Task<Client> AddAsync(Client client)
    {
        context.Clients.Add(client);
        await context.SaveChangesAsync();
        return client;
    }

    /// <summary>
    /// Загружает клиента из базы и сохраняет новые имя и телефон
    /// </summary>
    /// <param name="client">Клиент с идентификатором и новыми данными</param>
    /// <returns>True — клиент обновлён, False — клиент не найден</returns>
    public async Task<bool> UpdateAsync(Client client)
    {
        var existing = await context.Clients.FindAsync(client.ClientId);
        if (existing is null)
        {
            return false;
        }

        existing.FullName = client.FullName;
        existing.PhoneNumber = client.PhoneNumber;
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Удаляет клиента из базы по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>True — клиент удалён, False — клиент не найден</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        var client = await context.Clients.FindAsync(id);
        if (client is null)
        {
            return false;
        }

        context.Clients.Remove(client);
        await context.SaveChangesAsync();
        return true;
    }
}