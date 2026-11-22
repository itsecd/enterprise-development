using DryCleaner.Application.Contracts.ServiceInterfaces;
using DryCleaner.Application.Contracts.User;
using DryCleaner.Application.Mappers;
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DryCleaner.Application.Services;

/// <summary>
/// Сервис для управления пользователями.
/// </summary>
/// <param name="userRepository">Репозиторий для доступа к данным пользователей.</param>
/// <param name="passwordHasher">Сервис для хэширования паролей.</param>
public class UserService(
    IUserRepository userRepository,
    IPasswordHasher<User> passwordHasher) : IUserService
{
    /// <summary>
    /// Создает нового пользователя на основе предоставленных данных.
    /// </summary>
    /// <param name="dto">Данные для создания пользователя.</param>
    /// <returns>DTO созданного пользователя.</returns>
    public async Task<UserDto> Create(UserCreateUpdateDto dto)
    {
        var user = UserMapper.Map(dto);

        user.PasswordHash = passwordHasher.HashPassword(user, dto.Password);

        var result = await userRepository.Create(user);

        return UserMapper.Map(result);
    }

    /// <summary>
    /// Возвращает пользователя по идентификатору или null, если пользователь не найден.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <returns>Объект пользователя или null, если пользователь не найден.</returns>
    public async Task<UserDto?> Get(int id)
    {
        var user = await userRepository.Read(id);

        if (user == null)
            return null;

        return UserMapper.Map(user);
    }

    /// <summary>
    /// Получает список всех пользователей в виде объектов UserDto.
    /// </summary>
    /// <returns>Список объектов UserDto.</returns>
    public async Task<IList<UserDto>> GetAll()
    {
        return UserMapper.Map(await userRepository.ReadAll());
    }

    /// <summary>
    /// Обновляет данные пользователя по идентификатору.
    /// </summary>
    /// <param name="dto">Данные для обновления пользователя.</param>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <returns>Обновлённые данные пользователя.</returns>
    public async Task<UserDto> Update(UserCreateUpdateDto dto, int id)
    {
        var user = UserMapper.Map(dto);
        user.Id = id;

        user.PasswordHash = passwordHasher.HashPassword(user, dto.Password);

        var result = await userRepository.Update(user);

        return UserMapper.Map(result);
    }

    /// <summary>
    /// Удаляет пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя для удаления.</param>
    /// <returns>Значение true, если пользователь успешно удалён; в противном случае — false.</returns>
    public async Task<bool> Delete(int id)
    {
        var result = await userRepository.Delete(id);

        return result;
    }
}