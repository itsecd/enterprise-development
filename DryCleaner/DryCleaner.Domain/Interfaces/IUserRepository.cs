using DryCleaner.Domain.Entities;

namespace DryCleaner.Domain.Interfaces;

/// <summary>
/// Репозиторий для работы с пользователями.
/// </summary>
public interface IUserRepository : IRepository<User, int>
{
    /// <summary>
    /// Возвращает пользователя по имени пользователя.
    /// </summary>
    /// <param name="username">Имя пользователя.</param>
    /// <returns>Пользователь или null, если не найден.</returns>
    public Task<User?> GetByUsername(string username);
}