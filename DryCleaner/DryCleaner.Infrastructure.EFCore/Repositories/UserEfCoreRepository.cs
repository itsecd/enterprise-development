
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;
using DryCleaner.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace DryCleaner.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core-реализация репозитория <see cref="IUserRepository"/>
/// для работы с сущностью <see cref="User"/>.
/// </summary>
/// <remarks>
/// Использует <see cref="DryCleanerDbContext"/> для доступа к таблице пользователей.
/// </remarks>
public class UserEfCoreRepository(DryCleanerDbContext context)
    : IRepository<User, int>, IUserRepository
{
    /// <summary>
    /// Создаёт нового пользователя в базе данных.
    /// </summary>
    /// <param name="entity">Экземпляр пользователя для добавления.</param>
    /// <returns>Добавленный экземпляр пользователя.</returns>
    public async Task<User> Create(User entity)
    {
        var result = await context.Users.AddAsync(entity);
        await context.SaveChangesAsync();

        return result.Entity;
    }

    /// <summary>
    /// Удаляет пользователя по его идентификатору.
    /// </summary>
    /// <param name="entityId">Идентификатор пользователя для удаления.</param>
    /// <returns>
    /// <see langword="true"/>, если пользователь найден и удалён;
    /// иначе <see langword="false"/>.
    /// </returns>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.Users
            .FirstOrDefaultAsync(x => x.Id == entityId);

        if (entity == null)
            return false;

        context.Users.Remove(entity);
        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Возвращает пользователя по его идентификатору.
    /// </summary>
    /// <param name="entityId">Идентификатор искомого пользователя.</param>
    /// <returns>
    /// Найденный пользователь или <see langword="null"/>, если пользователь не найден.
    /// </returns>
    public async Task<User?> Read(int entityId) =>
        await context.Users.FirstOrDefaultAsync(x => x.Id == entityId);

    /// <summary>
    /// Возвращает список всех пользователей.
    /// </summary>
    /// <returns>Список всех пользователей из базы данных.</returns>
    public async Task<IList<User>> ReadAll() =>
        await context.Users.ToListAsync();

    /// <summary>
    /// Обновляет данные существующего пользователя.
    /// </summary>
    /// <param name="entity">Экземпляр пользователя с обновлёнными данными.</param>
    /// <returns>Актуальный экземпляр пользователя после сохранения изменений.</returns>
    public async Task<User> Update(User entity)
    {
        context.Users.Update(entity);
        await context.SaveChangesAsync();

        return (await Read(entity.Id))!;
    }

    /// <summary>
    /// Возвращает пользователя по его имени пользователя.
    /// </summary>
    /// <param name="username">Имя пользователя для поиска.</param>
    /// <returns>
    /// Найденный пользователь или <see langword="null"/>, если пользователь не найден.
    /// </returns>
    public async Task<User?> GetByUsername(string username) =>
        await context.Users.FirstOrDefaultAsync(x => x.Username == username);
}
