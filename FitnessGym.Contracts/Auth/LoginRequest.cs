using System.ComponentModel.DataAnnotations;

namespace FitnessGym.Contracts.Auth;

/// <summary>
/// Запрос на аутентификацию
/// </summary>
/// <param name="Username">Имя пользователя (обязательное поле)</param>
/// <param name="Password">Пароль (обязательное поле)</param>
public record LoginRequest(
    [Required] string Username,
    [Required] string Password);