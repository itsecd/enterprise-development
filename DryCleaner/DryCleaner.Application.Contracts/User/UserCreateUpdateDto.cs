namespace DryCleaner.Application.Contracts.User;

/// <summary>
/// DTO для Post, Put запросов к пользователям.
/// </summary>
/// <param name="Username">Имя пользователя.</param>
/// <param name="Password">Пароль пользователя.</param>
public record UserCreateUpdateDto(string Username, string Password);