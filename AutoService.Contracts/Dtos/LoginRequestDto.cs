namespace AutoService.Contracts.Dtos;

/// <summary>
/// Данные для входа в приложение
/// </summary>
public class LoginRequestDto
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// Пароль
    /// </summary>
    public required string Password { get; set; }
}
