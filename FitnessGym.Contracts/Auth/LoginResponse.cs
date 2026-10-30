namespace FitnessGym.Contracts.Auth;

/// <summary>
/// Ответ с JWT-токеном
/// </summary>
public record LoginResponse(string Token);