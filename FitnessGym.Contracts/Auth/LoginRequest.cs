using System.ComponentModel.DataAnnotations;

namespace FitnessGym.Contracts.Auth;

/// <summary>
/// Запрос на аутентификацию
/// </summary>
public record LoginRequest(
    [Required] string Username,
    [Required] string Password);