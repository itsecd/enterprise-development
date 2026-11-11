namespace DryCleaner.Application.Contracts.Auth;

/// <summary>
/// Данные для входа.
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Логин.
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// Пароль
    /// </summary>
    public required string Password { get; set; }
}