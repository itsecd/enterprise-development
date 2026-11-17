using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
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
    private readonly TestDatabase _database = new();
    private readonly AuthService _service;

    /// <summary>
    /// Сервис входа поверх базы с пользователем admin
    /// </summary>
    public AuthServiceTests()
    {
        var options = Options.Create(new JwtOptions
        {
            Issuer = "VetClinic.Tests",
            Audience = "VetClinic.Tests",
            Key = "test-signing-key-at-least-32-characters"
        });
        _service = new AuthService(new UserRepository(_database.Context), options, TimeProvider.System);
    }

    /// <summary>
    /// Верный пароль дает токен с издателем из настроек и сроком в будущем
    /// </summary>
    [Fact]
    public async Task Login_ValidPassword_ReturnsToken()
    {
        var token = await _service.LoginAsync(new LoginDto { Username = "admin", Password = "Vet2026!" }, TestContext.Current.CancellationToken);

        Assert.NotNull(token);
        Assert.Equal("VetClinic.Tests", new JsonWebToken(token.AccessToken).Issuer);
        Assert.True(token.ExpiresAt > DateTime.UtcNow);
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
