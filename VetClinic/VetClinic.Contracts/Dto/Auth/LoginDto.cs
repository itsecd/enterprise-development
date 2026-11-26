using System.ComponentModel.DataAnnotations;

namespace VetClinic.Contracts.Dto.Auth;

/// <summary>
/// Данные для входа
/// </summary>
public class LoginDto
{
    /// <summary>
    /// Имя для входа
    /// </summary>
    [Required]
    [MaxLength(100)]
    public required string Username { get; init; }

    /// <summary>
    /// Пароль
    /// </summary>
    [Required]
    [MaxLength(100)]
    public required string Password { get; init; }
}
