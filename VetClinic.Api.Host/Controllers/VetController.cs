using Microsoft.AspNetCore.Mvc;
using VetClinic.Application.Contracts.Vets;
using VetClinic.Domain.Shared.Enums;

namespace VetClinic.Api.Host.Controllers;
/// <summary>
/// Контроллер для управления кадровым составом ветеринарных врачей
/// </summary>
/// <param name="service">Сервис управления врачами</param>
/// <param name="logger">Логгер контроллера врачей</param>
public class VetController(IVetAppService service, ILogger<VetController> logger)
    : CrudControllerBase<VetDto, VetCreateUpdateDto, int>(service, logger)
{
    /// <summary>
    /// Возвращает список ветеринарных врачей, специализирующихся на указанном виде животных
    /// </summary>
    /// <param name="species">Вид животного</param>
    /// <returns>Список подходящих врачей</returns>
    [HttpGet("by-species/{species}")]
    public async Task<IActionResult> GetVetsBySpecies(Species species)
    {
        logger.LogInformation("Запрос на получение врачей для вида животных: {Species}", species);
        var result = await service.GetVetsBySpecies(species);
        return Ok(result);
    }
}
