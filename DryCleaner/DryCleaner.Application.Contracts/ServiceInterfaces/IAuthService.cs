using DryCleaner.Application.Contracts.Auth;

namespace DryCleaner.Application.Contracts.ServiceInterfaces;

/// <summary>
/// Определяет контракт для работы аутентификции
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Производит аутентификацию 
    /// </summary>
    /// <param name="request">Данные для аутентификации</param>
    /// <returns></returns>
    public Task<LoginResponse?> LoginAsync(LoginRequest request);
}