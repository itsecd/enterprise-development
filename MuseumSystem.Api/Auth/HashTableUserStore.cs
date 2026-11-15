using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;

namespace MuseumSystem.Api.Auth;

/// <summary>
/// Потокобезопасное хранилище пользователей на основе хэш-таблицы.
/// Пароли хранятся в виде SHA-256.
/// </summary>
public class HashTableUserStore
{
    private readonly ConcurrentDictionary<string, string> _users = new();

    public HashTableUserStore()
    {
        // Сидинг тестового пользователя
        Register("admin", "password");
        Register("manager", "password2");
        Register("visitor", "password3");
    }

    public void Register(string username, string password)
    {
        var hash = HashPassword(password);
        _users[username] = hash;
    }

    public bool TryRegister(string username, string password)
    {
        var hash = HashPassword(password);
        // Вернет false, если пользователь с таким логином уже ест
        return _users.TryAdd(username, hash);
    }

    public bool Validate(string username, string password)
    {
        if (!_users.TryGetValue(username, out var storedHash))
            return false;

        return storedHash == HashPassword(password);
    }

    private static string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}