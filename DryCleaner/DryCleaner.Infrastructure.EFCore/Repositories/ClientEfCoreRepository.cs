using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;
using DryCleaner.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace DryCleaner.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core-реализация репозитория <see cref="IRepository{TEntity, TKey}"/> 
/// для работы с сущностью <see cref="Client"/>.
/// </summary>
/// <remarks>
/// Использует <see cref="DryCleanerDbContext"/> для доступа к таблице клиентов.
/// </remarks>
public class ClientEfCoreRepository(DryCleanerDbContext context) : IRepository<Client, int>
{
    /// <summary>
    /// Создаёт нового клиента в базе данных.
    /// </summary>
    /// <param name="entity">Экземпляр клиента для добавления.</param>
    /// <returns>Добавленный экземпляр клиента.</returns>
    public async Task<Client> Create(Client entity)
    {
        var result = await context.Clients.AddAsync(entity);
        await context.SaveChangesAsync();

        return entity;
    }

    /// <summary>
    /// Удаляет клиента по его идентификатору.
    /// </summary>
    /// <param name="entityId">Идентификатор клиента для удаления.</param>
    /// <returns>
    /// <see langword="true"/>, если клиент найден и удалён; 
    /// иначе <see langword="false"/>.
    /// </returns>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.Clients
            .FirstOrDefaultAsync(context => context.Id == entityId);

        if (entity == null)
            return false;

        context.Clients.Remove(entity);
        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Возвращает клиента по его идентификатору.
    /// </summary>
    /// <param name="entityId">Идентификатор искомого клиента.</param>
    /// <returns>
    /// Найденный клиент или <see langword="null"/>, если клиент не найден.
    /// </returns>
    public async Task<Client?> Read(int entityId) => await context.Clients.FirstOrDefaultAsync(context => context.Id == entityId);

    /// <summary>
    /// Возвращает список всех клиентов.
    /// </summary>
    /// <returns>Список всех клиентов из базы данных.</returns>
    public async Task<IList<Client>> ReadAll() => await context.Clients.ToListAsync();

    /// <summary>
    /// Обновляет данные существующего клиента.
    /// </summary>
    /// <param name="entity">Экземпляр клиента с обновлёнными данными.</param>
    /// <returns>Актуальный экземпляр клиента после сохранения изменений.</returns>
    public async Task<Client> Update(Client entity)
    {
        context.Clients.Update(entity);
        await context.SaveChangesAsync();

        return (await Read(entity.Id))!;
    }
}
