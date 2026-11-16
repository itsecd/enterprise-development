using CarWash.Application.Contracts;
using CarWash.Application.Contracts.Orders;

namespace CarWash.Api.Host.Controllers;

/// <summary>
/// Контроллер CRUD-операций над заказами автомойки
/// </summary>
public class OrderController(
    IApplicationService<OrderDto, OrderCreateUpdateDto, int> crudService,
    ILogger<OrderController> logger)
    : CrudControllerBase<OrderDto, OrderCreateUpdateDto, int>(
        crudService, logger);