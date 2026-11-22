namespace DryCleaner.Application.Contracts.User;

/// <summary>
/// DTO для Get запросов к пользователям.
/// </summary>
/// <param name="Id">Идентификатор пользователя.</param>
/// <param name="Username">Имя пользователя.</param>
public record UserDto(int Id, string Username);