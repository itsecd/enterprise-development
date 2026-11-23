using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using VetClinic.Application.Auth;
using VetClinic.Contracts.Dto.Auth;
using VetClinic.Infrastructure.SqlServer.Repositories;
using VetClinic.Tests.Fixtures;

namespace VetClinic.Tests;

/// <summary>
/// Тесты входа: пароль проверяется по хешу из начальных данных, токен подписан и содержит издателя
/// </summary>
public sealed class AuthServiceTests : IAsyncDisposable
{
    private const string Issuer = "VetClinic.Tests";
    private const string Audience = "VetClinic.Tests.Client";
    private const string SigningKey = "test-signing-key-at-least-32-characters";
    private readonly TestDatabase _database = new();
    private readonly AuthService _service;

    /// <summary>
    /// Сервис входа поверх базы с пользователем admin
    /// </summary>
    public AuthServiceTests()
    {
        var options = Options.Create(new JwtOptions
        {
            Issuer = Issuer,
            Audience = Audience,
            Key = SigningKey
        });
        _service = new AuthService(new UserRepository(_database.Context), options, TimeProvider.System);
    }

    [Fact]
    public async Task Login_ValidPassword_ReturnsValidSignedToken()
    {
        var token = await _service.LoginAsync(new LoginDto { Username = "admin", Password = "Vet2026!" }, TestContext.Current.CancellationToken);

        Assert.NotNull(token);
        var validation = await new JsonWebTokenHandler().ValidateTokenAsync(token.AccessToken, new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SigningKey)),
            ValidIssuer = Issuer,
            ValidAudience = Audience,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        });
        Assert.True(validation.IsValid, validation.Exception?.Message);
        Assert.Equal(new DateTimeOffset(token.ExpiresAt).ToUnixTimeSeconds(),
            new DateTimeOffset(validation.SecurityToken.ValidTo).ToUnixTimeSeconds());
    }

    /// <summary>
    /// Неверный пароль или неизвестный пользователь не дают токен
    /// </summary>
    [Theory]
    [InlineData("admin", "wrong")]
    [InlineData("nobody", "Vet2026!")]
    public async Task Login_InvalidCredentials_ReturnsNull(string username, string password)
    {
        var token = await _service.LoginAsync(new LoginDto { Username = username, Password = password }, TestContext.Current.CancellationToken);

        Assert.Null(token);
    }

    /// <inheritdoc/>
    public ValueTask DisposeAsync() => _database.DisposeAsync();
}
