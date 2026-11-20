namespace VetClinic.Domain.Models;

/// <summary>
/// Пользователь, которому разрешено изменять данные через API
/// </summary>
public class User
{
    /// <summary>
    /// Уникальный идентификатор пользователя
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Имя для входа
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// Хеш пароля, сам пароль не хранится
    /// </summary>
    public required string PasswordHash { get; set; }
}
