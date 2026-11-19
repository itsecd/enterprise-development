using AutoService.Domain.Models;

namespace AutoService.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория пользователей
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Получить пользователя по имени
    /// </summary>
    public Task<User?> GetByUsernameAsync(string username);
}
