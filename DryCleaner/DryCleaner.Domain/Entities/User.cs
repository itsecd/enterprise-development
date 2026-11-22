using System.ComponentModel.DataAnnotations.Schema;


namespace DryCleaner.Domain.Entities;

/// <summary>
/// Пользователь системы.
/// </summary>
[Table("user")]
public class User
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    [Column("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    [Column("username")]
    public required string Username { get; set; } = null!;

    /// <summary>
    /// Хэш пароля.
    /// </summary>
    [Column("password_hash")]
    public required string PasswordHash { get; set; } = null!;
}

