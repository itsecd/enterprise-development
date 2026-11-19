using AutoService.Domain.Interfaces;
using AutoService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Infrastructure.Persistence.Repositories;

/// <summary>
/// Реализация репозитория пользователей на Entity Framework Core
/// </summary>
public class EfUserRepository(AutoServiceDbContext context) : IUserRepository
{
    /// <summary>
    /// Получить пользователя по имени
    /// </summary>
    public async Task<User?> GetByUsernameAsync(string username) =>
        await context.Users.FirstOrDefaultAsync(u => u.Username == username);
}
