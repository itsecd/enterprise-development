using Carwash.Application.Contracts;
using Carwash.Application.Contracts.Orders;

namespace Carwash.Api.Controllers;

/// <summary>
/// Контроллер для CRUD-операций над заказами
/// </summary>
/// <param name="service">Служба для CRUD операций с Dto заказов</param>
/// <param name="logger">Логгер</param>
public class OrderController(IApplicationService<OrderDto, OrderCreateUpdateDto, int> service, ILogger<OrderController> logger)
    : CrudControllerBase<OrderDto, OrderCreateUpdateDto, int>(service, logger);
