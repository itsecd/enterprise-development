using System.ComponentModel.DataAnnotations;

namespace MuseumSystem.Contracts.Models;

/// <summary>
/// Запрос на авторизацию
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    [Required]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Пароль
    /// </summary>
    [Required]
    public string Password { get; set; } = string.Empty;
}