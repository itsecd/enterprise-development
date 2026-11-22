using DryCleaner.Application.Contracts.ServiceInterfaces;
using DryCleaner.Application.Contracts.User;

namespace DryCleaner.Api.Controllers;

/// <summary>
/// Контроллер для управления пользователями.
/// </summary>
/// <param name="service">Сервис для работы с пользователями.</param>
/// <param name="logger">Логгер для записи событий контроллера.</param>
public class UserController(IUserService service, ILogger<UserController> logger) :
    CrudControllerBase<UserDto, UserCreateUpdateDto, int>(service, logger);