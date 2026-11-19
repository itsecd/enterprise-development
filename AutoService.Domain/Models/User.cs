namespace AutoService.Domain.Models;

/// <summary>
/// Пользователь серверного приложения
/// </summary>
public class User
{
    /// <summary>
    /// Уникальный идентификатор пользователя
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// Хеш пароля
    /// </summary>
    public required string PasswordHash { get; set; }
}
