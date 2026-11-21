namespace DryCleaner.Application.Contracts.Auth;

/// <summary>
/// Ответ с JWT-токеном.
/// </summary>
public class LoginResponse
{
    /// <summary>
    /// JWT-токен.
    /// </summary>
    public required string Token { get; set; }
}