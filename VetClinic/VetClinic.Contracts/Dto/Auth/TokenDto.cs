namespace VetClinic.Contracts.Dto.Auth;

/// <summary>
/// Выданный токен доступа
/// </summary>
public class TokenDto
{
    /// <summary>
    /// JWT, передается в заголовке Authorization: Bearer
    /// </summary>
    public required string AccessToken { get; init; }

    /// <summary>
    /// Момент, после которого токен недействителен (UTC)
    /// </summary>
    public required DateTime ExpiresAt { get; init; }
}
