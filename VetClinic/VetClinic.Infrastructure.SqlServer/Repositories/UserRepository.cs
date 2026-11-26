using Microsoft.EntityFrameworkCore;
using VetClinic.Domain.Models;
using VetClinic.Domain.Repositories;

namespace VetClinic.Infrastructure.SqlServer.Repositories;

/// <summary>
/// Репозиторий пользователей
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class UserRepository(VetClinicDbContext context) : IUserRepository
{
    /// <inheritdoc/>
    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default) =>
        await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
}
