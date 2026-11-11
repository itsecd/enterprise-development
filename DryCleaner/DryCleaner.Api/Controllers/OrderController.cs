using DryCleaner.Application.Contracts.Order;
using DryCleaner.Application.Contracts.ServiceInterfaces;

namespace DryCleaner.Api.Controllers;

/// <summary>
/// Контроллер для управления заказами.
/// </summary>
/// <param name="service">Сервис для работы с заказами.</param>
/// <param name="logger">Логгер для записи событий контроллера.</param>
public class OrderController(IOrderService service, ILogger<OrderController> logger) :
    CrudControllerBase<OrderDto, OrderCreateUpdateDto, int>(service, logger);
