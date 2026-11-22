using DryCleaner.Application.Contracts.Auth;
using DryCleaner.Domain.Entities;

namespace DryCleaner.Application.Mappers;

/// <summary>
/// Преобразует объекты авторизации в сущности User и обратно.
/// </summary>
internal class AuthMapper
{
    /// <summary>
    /// Создает экземпляр сущности User на основе данных для входа.
    /// </summary>
    /// <param name="request">Данные для аутентификации.</param>
    /// <returns>Новая экземпляр сущности User.</returns>
    public static User Map(LoginRequest request)
    {
        return new User { Id = 0, Username = request.Username, PasswordHash = "" };
    }

    /// <summary>
    /// Создает ответ с JWT-токеном.
    /// </summary>
    /// <param name="token">JWT-токен.</param>
    /// <returns>Ответ с JWT-токеном.</returns>
    public static LoginResponse Map(string token)
    {
        return new LoginResponse { Token = token };
    }
}