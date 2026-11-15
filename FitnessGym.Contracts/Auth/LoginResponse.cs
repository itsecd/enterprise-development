namespace FitnessGym.Contracts.Auth;

/// <summary>
/// Ответ с JWT-токеном
/// </summary>
/// <param name="Token">JWT-токен доступа</param>
public record LoginResponse(string Token);