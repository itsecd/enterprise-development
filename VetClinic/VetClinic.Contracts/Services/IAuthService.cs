using VetClinic.Contracts.Dto.Auth;

namespace VetClinic.Contracts.Services;

/// <summary>
/// Аутентификация пользователей и выдача JWT
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Проверить имя и пароль и выдать токен
    /// </summary>
    /// <param name="dto">Данные для входа</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Токен или null, если имя или пароль неверны</returns>
    public Task<TokenDto?> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default);
}
