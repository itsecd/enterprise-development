using VetClinic.Domain.Models;
using VetClinic.Domain.Repositories;
using VetClinic.Domain.Seeds;

namespace VetClinic.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий пользователей в памяти
/// </summary>
/// <param name="seed">Начальные данные</param>
public class UserRepository(VetClinicSeed seed) : IUserRepository
{
    /// <inheritdoc/>
    public Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default) =>
        Task.FromResult(seed.Users.Find(user => user.Username == username));
}
