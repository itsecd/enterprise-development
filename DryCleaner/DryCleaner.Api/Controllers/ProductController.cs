using DryCleaner.Application.Contracts.Product;
using DryCleaner.Application.Contracts.ServiceInterfaces;

namespace DryCleaner.Api.Controllers;

/// <summary>
/// Контроллер для управления продуктами.
/// </summary>
/// <param name="service">Сервис для управления продуктами.</param>
/// <param name="logger">Логгер для записи событий контроллера.</param>
public class ProductController(IProductService service, ILogger<ProductController> logger) :
    CrudControllerBase<ProductDto, ProductCreateUpdateDto, int>(service, logger);
