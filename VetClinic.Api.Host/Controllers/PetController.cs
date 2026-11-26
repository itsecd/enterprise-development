using Microsoft.AspNetCore.Mvc;
using VetClinic.Application.Contracts.Pets;

namespace VetClinic.Api.Host.Controllers;

/// <summary>
/// Контроллер для управления картотекой питомцев клиники
/// </summary>
/// <param name="service">Сервис управления питомцами</param>
/// <param name="logger">Логгер контроллера питомцев</param>
public class PetController(IPetAppService service, ILogger<PetController> logger)
    : CrudControllerBase<PetDto, PetCreateUpdateDto, int>(service, logger)
{
    /// <summary>
    /// Возвращает список всех зарегистрированных питомцев, отсортированный по кличке в алфавитном порядке.
    /// </summary>
    /// <returns>Отсортированный список DTO питомцев.</returns>
    [HttpGet("sorted")]
    public async Task<IActionResult> GetSorted()
    {
        logger.LogInformation("Запрос на получение отсортированного списка питомцев");
        var result = await service.GetSortedByName();
        return Ok(result);
    }
}
