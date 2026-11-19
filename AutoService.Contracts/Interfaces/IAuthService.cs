using AutoService.Contracts.Dtos;

namespace AutoService.Contracts.Interfaces;

/// <summary>
/// Сервис аутентификации пользователей
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Проверить учетные данные и создать JWT
    /// </summary>
    public Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginDto);
}
