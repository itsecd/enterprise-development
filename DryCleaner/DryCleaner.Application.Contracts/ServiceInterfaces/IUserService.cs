using DryCleaner.Application.Contracts.User;

namespace DryCleaner.Application.Contracts.ServiceInterfaces;

/// <summary>
/// Определяет контракт для работы с пользователями.
/// </summary>
public interface IUserService : IAplicationService<UserDto, UserCreateUpdateDto, int>;