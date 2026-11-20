using System.ComponentModel.DataAnnotations;

namespace VetClinic.Application.Auth;

/// <summary>
/// Параметры выпуска и проверки JWT, секция Jwt в конфигурации
/// </summary>
public class JwtOptions
{
    /// <summary>
    /// Имя секции в конфигурации
    /// </summary>
    public const string SectionName = "Jwt";

    /// <summary>
    /// Кто выпустил токен
    /// </summary>
    [Required]
    public required string Issuer { get; init; }

    /// <summary>
    /// Для кого предназначен токен
    /// </summary>
    [Required]
    public required string Audience { get; init; }

    /// <summary>
    /// Ключ подписи HMAC-SHA256, не короче 32 символов
    /// </summary>
    [Required]
    [MinLength(32)]
    public required string Key { get; init; }

    /// <summary>
    /// Время жизни токена в минутах
    /// </summary>
    [Range(1, 1440)]
    public int LifetimeMinutes { get; init; } = 60;
}
