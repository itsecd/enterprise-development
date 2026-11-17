using VetClinic.Domain.Models;

namespace VetClinic.Domain.Repositories;

/// <summary>
/// Репозиторий пользователей
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Найти пользователя по имени для входа
    /// </summary>
    /// <param name="username">Имя для входа</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
}
