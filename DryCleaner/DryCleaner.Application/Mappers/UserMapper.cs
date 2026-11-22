using DryCleaner.Application.Contracts.Client;
using DryCleaner.Application.Contracts.User;
using DryCleaner.Domain.Entities;

namespace DryCleaner.Application.Mappers;

/// <summary>
/// Маппер для преобразования пользователей.
/// </summary>
public static class UserMapper
{
    /// <summary>
    /// Преобразует пользователя в DTO.
    /// </summary>
    /// <param name="user">Пользователь.</param>
    /// <returns>DTO пользователя.</returns>
    public static UserDto Map(User user)
    {
        return new UserDto(user.Id, user.Username);
    }

    /// <summary>
    /// Преобразует список пользователей в список DTO.
    /// </summary>
    /// <param name="users">Список пользователей.</param>
    /// <returns>Список DTO пользователей.</returns>
    public static IList<UserDto> Map(IList<User> users)
    {
        return users.Select(Map).ToList();
    }

    /// <summary>
    /// Преобразует DTO в доменную сущность.
    /// </summary>
    /// <param name="dto">Пользователь.</param>
    /// <returns>Список DTO пользователей.</returns>
    public static User Map(UserCreateUpdateDto dto)
    {
        return new User { Id = 0, Username = dto.Username, PasswordHash = string.Empty };
    }
}