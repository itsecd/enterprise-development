namespace MuseumSystem.Api.Auth;

/// <summary>
/// Модель тестового пользователя для загрузки из конфигурации.
/// </summary>
public class TestUser
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}