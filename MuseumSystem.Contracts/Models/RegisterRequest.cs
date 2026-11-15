namespace MuseumSystem.Contracts.Models;

/// <summary>
/// Запрос на регистрацию нового пользователя
/// </summary>
public class RegisterRequest
{
	/// <summary>
	/// Уникальный логин (минимум 3 символа)
	/// </summary>
	public required string Username { get; set; }

	/// <summary>
	/// Пароль (минимум 6 символов)
	/// </summary>
	public required string Password { get; set; }
}