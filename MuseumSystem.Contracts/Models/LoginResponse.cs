namespace MuseumSystem.Contracts.Models;

/// <summary>
/// Ответ с JWT токеном
/// </summary>
public class LoginResponse
{
    /// <summary>
    /// JWT токен для доступа к API
    /// </summary>
    public string Token { get; set; } = string.Empty;
}